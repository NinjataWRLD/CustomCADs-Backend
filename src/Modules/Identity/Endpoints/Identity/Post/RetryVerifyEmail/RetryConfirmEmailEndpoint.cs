using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.Identity.Post.RetryVerifyEmail;

public sealed class RetryConfirmEmailEndpoint(IRequestSender sender, LinkGenerator links)
	: Endpoint<RetryConfirmEmailRequest>
{
	public override void Configure()
	{
		Post("email/confirm/retry");
		Group<IdentityGroup>();
		Description(d => d
			.WithName(IdentityNames.RetryConfirmEmail)
			.WithSummary("Retry Send Email")
			.WithDescription("Receive another verification email")
		);
	}

	public override async Task HandleAsync(RetryConfirmEmailRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new VerificationEmailCommand(
				Username: req.Username,
				GetUri: ect => links.GetUriByName(
					httpContext: HttpContext,
					endpointName: IdentityNames.ConfirmEmail,
					values: new { username = req.Username, token = ect, idempotencyKey = Guid.NewGuid() }
				) ?? throw new InvalidOperationException("Unable to generate confirmation link.")
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
