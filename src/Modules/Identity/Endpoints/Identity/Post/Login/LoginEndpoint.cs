using CustomCADs.Identity.Application.Users.Commands.Internal.Login;
using CustomCADs.Identity.Application.Users.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Login;

public sealed class LoginEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
	: Endpoint<LoginRequest>
{
	public override void Configure()
	{
		Post("login");
		Group<IdentityGroup>();
		AllowAnonymous();
		Description(d => d
			.WithName(IdentityNames.Login)
			.WithSummary("Login")
			.WithDescription("Log in to your account")
		);
	}

	public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
	{
		TokensDto tokens = await sender.SendCommandAsync(
			new LoginUserCommand(
				Username: req.Username,
				Password: req.Password,
				LongerExpireTime: req.RememberMe ?? false
			),
			ct
		).ConfigureAwait(false);

		HttpContext.SaveAllCookies(
			domain: settings.Value.Domain,
			username: req.Username,
			tokens: tokens
		);
		await SendOkAsync("Welcome back!").ConfigureAwait(false);
	}
}
