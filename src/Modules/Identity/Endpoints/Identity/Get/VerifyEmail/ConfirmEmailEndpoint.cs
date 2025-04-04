namespace CustomCADs.Identity.Endpoints.Identity.Get.VerifyEmail;

public sealed class ConfirmEmailEndpoint(IUserService service)
    : Endpoint<ConfirmEmailRequest>
{
    public override void Configure()
    {
        Get("email/confirm/{username}");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.ConfirmEmail)
            .WithSummary("Confirm Email")
            .WithDescription("Confirm the verification email")
        );
    }

    public override async Task HandleAsync(ConfirmEmailRequest req, CancellationToken ct)
    {
        TokensDto tokens = await service.ConfirmEmailAsync(
            username: req.Username,
            token: req.Token.Replace(' ', '+')
        ).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(tokens.AccessToken.Value, tokens.AccessToken.EndDate);
        HttpContext.SaveRefreshTokenCookie(tokens.RefreshToken.Value, tokens.RefreshToken.EndDate);
        HttpContext.SaveRoleCookie(tokens.Role, tokens.RefreshToken.EndDate);
        HttpContext.SaveUsernameCookie(req.Username, tokens.RefreshToken.EndDate);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
