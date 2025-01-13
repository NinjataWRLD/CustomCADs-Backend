using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.SignUp.RetryVerifyEmail;

using static ApiMessages;
using static StatusCodes;

public sealed class RetryConfirmEmailEndpoint(IUserService service, LinkGenerator links)
    : Endpoint<RetryConfirmEmailRequest>
{
    public override void Configure()
    {
        Get("email/confirm/{username}/retry");
        Group<SignUpGroup>();
        Description(d => d
            .WithName(SignUpNames.RetryConfirmEmail)
            .WithSummary("02. Retry Send Email")
            .WithDescription("Receive another verification email")
        );
    }

    public override async Task HandleAsync(RetryConfirmEmailRequest req, CancellationToken ct)
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

        string token = await service.GenerateEmailConfirmationTokenAsync(req.Username).ConfigureAwait(false);
        string uri = links.GetUriByName(HttpContext, SignUpNames.ConfirmEmail, new { username = req.Username, token = token })
            ?? throw new InvalidOperationException("Unable to generate confirmation link.");

        await service.SendVerificationEmailAsync(user, uri).ConfigureAwait(false);

        await SendOkAsync("Check your email.").ConfigureAwait(false);
    }
}
