namespace CustomCADs.Identity.Endpoints.SignUp.RetryVerifyEmail;

using CustomCADs.Identity.Application.Common.Contracts;
using static ApiMessages;
using static StatusCodes;

public sealed class RetryVerifyEmailEndpoint(IUserService service)
    : Endpoint<RetryVerifyEmailRequest>
{
    public override void Configure()
    {
        Get("email/verify/{username}/retry");
        Group<SignUpGroup>();
        Description(d => d
            .WithSummary("02. Retry Send Email")
            .WithDescription("Receive another verification email")
        );
    }

    public override async Task HandleAsync(RetryVerifyEmailRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("Name", UserNotFound, req.Username));
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        if (user.EmailConfirmed)
        {
            ValidationFailures.Add(new("Email", EmailAlreadyVerified));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }
        await service.SendVerificationEmailAsync(user).ConfigureAwait(false);

        await SendOkAsync("Check your email.").ConfigureAwait(false);
    }
}
