namespace CustomCADs.Identity.Endpoints.Identity.Post.RefreshToken;

public sealed class RefreshTokenEndpoint(IUserService userService)
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
        const string AccessTokenRefreshed = "The JS Web Token has been renewed.";
        const string NewRefreshTokenGranted = "A new Refresh Token has been granted.";

        RefreshDto dto = await userService.RefreshAsync(
            rt: HttpContext.GetRefreshTokenCookie()
        ).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(dto.AccessToken.Value, dto.AccessToken.EndDate);

        if (dto.RefreshToken is null)
        {
            await SendOkAsync(AccessTokenRefreshed).ConfigureAwait(false);
            return;
        }

        HttpContext.SaveRefreshTokenCookie(dto.RefreshToken.Value, dto.RefreshToken.EndDate);
        HttpContext.SaveRoleCookie(dto.Role, dto.RefreshToken.EndDate);
        HttpContext.SaveUsernameCookie(dto.Username, dto.RefreshToken.EndDate);

        await SendOkAsync($"{AccessTokenRefreshed} {NewRefreshTokenGranted}").ConfigureAwait(false);
    }
}
