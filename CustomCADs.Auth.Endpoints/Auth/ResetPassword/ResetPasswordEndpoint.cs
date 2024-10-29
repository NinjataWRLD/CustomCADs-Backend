using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Endpoints.Auth.ResetPassword;

using static Helpers.ApiMessages;
using static StatusCodes;

public class ResetPasswordEndpoint(IUserService service) : Endpoint<ResetPasswordRequest>
{
    public override void Configure()
    {
        Post("ResetPassword");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByEmailAsync(req.Email).ConfigureAwait(false);
        if (user == null)
        {
            ValidationFailures.Add(new()
            {
                ErrorMessage = string.Format(NotFound, "User"),
            });
            await SendErrorsAsync(Status404NotFound);
            return;
        }

        string encodedToken = req.Token.Replace(' ', '+');
        IdentityResult result = await service.ResetPasswordAsync(user, encodedToken, req.NewPassword).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            var failures = result.Errors.Select(e => new ValidationFailure()
            {
                ErrorMessage = e.Description
            });
            ValidationFailures.AddRange(failures);

            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        await SendOkAsync("Done!").ConfigureAwait(false);
    }
}
