using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.Identity.Get.RetryVerifyEmail;

public sealed class RetryConfirmEmailEndpoint(IUserService service, LinkGenerator links)
    : Endpoint<RetryConfirmEmailRequest>
{
    public override void Configure()
    {
        Get("email/confirm/{username}/retry");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.RetryConfirmEmail)
            .WithSummary("Retry Send Email")
            .WithDescription("Receive another verification email")
        );
    }

    public override async Task HandleAsync(RetryConfirmEmailRequest req, CancellationToken ct)
    {
        await service.SendVerificationEmailAsync(
            username: req.Username,
            getUri: ect => links.GetUriByName(
                httpContext: HttpContext,
                endpointName: IdentityNames.ConfirmEmail,
                values: new { username = req.Username, token = ect }
            ) ?? throw new InvalidOperationException("Unable to generate confirmation link.")
        ).ConfigureAwait(false);

        await SendOkAsync("Check your email.").ConfigureAwait(false);
    }
}
