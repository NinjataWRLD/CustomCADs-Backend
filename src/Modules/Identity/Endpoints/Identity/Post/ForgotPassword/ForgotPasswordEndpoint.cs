using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;
using CustomCADs.Shared.Endpoints.Attributes;

namespace CustomCADs.Identity.Endpoints.Identity.Post.ForgotPassword;

public sealed class ForgotPasswordEndpoint(IRequestSender sender)
	: Endpoint<ForgotPasswordRequest>
{
	public override void Configure()
	{
		Post("password/forgot");
		Group<IdentityGroup>();
		Description(d => d
			.WithName(IdentityNames.ForgotPassword)
			.WithSummary("Reset Password Email")
			.WithDescription("Receive an Email with a link to reset your Password")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new ResetPasswordEmailCommand(
				Email: req.Email
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
