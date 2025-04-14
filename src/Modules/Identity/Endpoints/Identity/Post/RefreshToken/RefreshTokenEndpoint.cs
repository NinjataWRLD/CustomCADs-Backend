using CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;
using CustomCADs.Identity.Application.Users.Dtos;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Endpoints.Identity.Post.RefreshToken;

public sealed class RefreshTokenEndpoint(IRequestSender sender, IOptions<CookieSettings> settings)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("refresh");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.Refresh)
            .WithSummary("Refresh")
            .WithDescription("Refresh your login")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        TokensDto tokens = await sender.SendCommandAsync(
            new RefreshUserCommand(
                Token: HttpContext.GetRefreshTokenCookie()
            ),
            ct
        ).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(tokens.AccessToken, settings.Value.Domain);
        HttpContext.SaveCsrfTokenCookie(tokens.CsrfToken, settings.Value.Domain);
        await SendOkAsync("The JSON Web Token has been renewed.").ConfigureAwait(false);
    }
}
