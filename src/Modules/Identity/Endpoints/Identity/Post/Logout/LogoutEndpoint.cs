using CustomCADs.Identity.Application.Users.Commands.Internal.Logout;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Shared.Endpoints.Attributes;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Logout;

public sealed class LogoutEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
	: EndpointWithoutRequest<string>
{
	public override void Configure()
	{
		Post("logout");
		Group<IdentityGroup>();
		Description(d => d
			.WithName(IdentityNames.Logout)
			.WithSummary("Log out")
			.WithDescription("Log out of your account")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new LogoutUserCommand(
				RefreshToken: HttpContext.GetRefreshTokenCookie()
			),
			ct
		).ConfigureAwait(false);

		HttpContext.DeleteAllCookies(settings.Value.Domain);
	}
}
