using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Endpoints.SignIn.ResetPassword;

using static ApiMessages;
using static StatusCodes;

public sealed class ResetPasswordEndpoint(IUserService service)
    : Endpoint<ResetPasswordRequest>
{
    public override void Configure()
    {
        Post("password/reset");
        Group<SignInGroup>();
        Description(d => d
            .WithSummary("05. Reset Password")
            .WithDescription("Reset your Password with the token from the email")
        );
    }

    public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByEmailAsync(req.Email).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("Email", UserNotFound, req.Email));
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        string encodedToken = req.Token.Replace(' ', '+');
        IdentityResult result = await service.ResetPasswordAsync(user, encodedToken, req.NewPassword).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            ValidationFailures.AddRange(result.Errors
                .Select(e => new ValidationFailure(e.Code, e.Description)));

            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        await SendOkAsync("Done!").ConfigureAwait(false);
    }
}
