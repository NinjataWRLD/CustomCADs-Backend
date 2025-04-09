using CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;
using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Endpoints.Identity.Post.RefreshToken;

public sealed class RefreshTokenEndpoint(IRequestSender sender)
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
        AccessTokenDto jwt = await sender.SendCommandAsync(
            new RefreshUserCommand(
                Token: HttpContext.GetRefreshTokenCookie()
            ),
            ct
        ).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(jwt.Value, jwt.ExpiresAt);
        await SendOkAsync("The JSON Web Token has been renewed.").ConfigureAwait(false);
    }
}
