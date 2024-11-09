namespace CustomCADs.Auth.Endpoints.SignUp.RetryVerifyEmail;

using static ApiMessages;
using static StatusCodes;

public class RetryVerifyEmailEndpoint(IUserService service)
    : Endpoint<RetryVerifyEmailRequest>
{
    public override void Configure()
    {
        Get("retryVerifyEmail/{username}");
        Group<SignUpGroup>();
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
