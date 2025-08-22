using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using CustomCADs.Shared.Endpoints.Attributes;

namespace CustomCADs.Identity.Endpoints.Identity.Post.RetryVerifyEmail;

public sealed class RetryConfirmEmailEndpoint(IRequestSender sender)
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
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(RetryConfirmEmailRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new VerificationEmailCommand(req.Username),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
