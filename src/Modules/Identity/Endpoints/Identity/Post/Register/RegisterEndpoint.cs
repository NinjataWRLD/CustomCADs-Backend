using CustomCADs.Identity.Application.Users.Commands.Internal.Register;
using CustomCADs.Identity.Application.Users.Commands.Internal.VerificationEmail;
using CustomCADs.Shared.Endpoints.Attributes;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

public sealed class RegisterEndpoint(IRequestSender sender)
	: Endpoint<RegisterRequest>
{
	public override void Configure()
	{
		Post("register");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(d => d
			.WithName(IdentityNames.Register)
			.WithSummary("Register")
			.WithDescription("Register an Account")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new RegisterUserCommand(
				Role: req.Role,
				Username: req.Username,
				Email: req.Email,
				Password: req.Password,
				FirstName: req.FirstName,
				LastName: req.LastName
			),
			ct
		).ConfigureAwait(false);

		await sender.SendCommandAsync(
			new VerificationEmailCommand(
				Username: req.Username
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync().ConfigureAwait(false);
	}
}
