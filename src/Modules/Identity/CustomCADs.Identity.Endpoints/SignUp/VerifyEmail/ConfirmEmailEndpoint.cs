namespace CustomCADs.Identity.Endpoints.SignUp.VerifyEmail;

public sealed class ConfirmEmailEndpoint(IUserService userService, ITokenService tokenService)
    : Endpoint<ConfirmEmailRequest>
{
    public override void Configure()
    {
        Get("email/confirm/{username}");
        Group<SignUpGroup>();
        Description(d => d
            .WithName(SignUpNames.ConfirmEmail)
            .WithSummary("03. Confirm Email")
            .WithDescription("Confirm the verification email")
        );
    }

    public override async Task HandleAsync(ConfirmEmailRequest req, CancellationToken ct)
    {
        AppUser user = await userService.FindByNameAsync(req.Username).ConfigureAwait(false);

        string decodedEct = req.Token.Replace(' ', '+');
        await userService.ConfirmEmailAsync(user, decodedEct).ConfigureAwait(false);
        
        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.AccountId, req.Username, role);
        RefreshTokenDto rt = await userService.UpdateRefreshTokenAsync(user.Id, longerSession: false).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(jwt.Value, jwt.EndDate);
        HttpContext.SaveRefreshTokenCookie(rt.Value, rt.EndDate);
        HttpContext.SaveRoleCookie(role, rt.EndDate);
        HttpContext.SaveUsernameCookie(req.Username, rt.EndDate);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
