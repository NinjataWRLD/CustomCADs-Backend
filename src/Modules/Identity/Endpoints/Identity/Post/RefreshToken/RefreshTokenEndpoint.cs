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
        AccessTokenDto jwt = await userService.RefreshAsync(
            rt: HttpContext.GetRefreshTokenCookie()
        ).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(jwt.Value, jwt.EndDate);
        await SendOkAsync("The JSON Web Token has been renewed.").ConfigureAwait(false);
    }
}
