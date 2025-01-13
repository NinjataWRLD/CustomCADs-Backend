﻿namespace CustomCADs.Identity.Endpoints.SignIn.Login;

public sealed class LoginEndpoint(IUserService userService)
    : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("login");
        Group<SignInGroup>();
        AllowAnonymous();
        Description(d => d
            .WithName(SignInNames.Login)
            .WithSummary("01. Login")
            .WithDescription("Log in to your account")
        );
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        LoginDto dto = await userService.LoginAsync(new(req.Username, req.Password)).ConfigureAwait(false);
       
        HttpContext.SaveAccessTokenCookie(dto.AccessToken.Value, dto.AccessToken.EndDate);
        HttpContext.SaveRefreshTokenCookie(dto.RefreshToken.Value, dto.RefreshToken.EndDate);
        HttpContext.SaveRoleCookie(dto.Role, dto.RefreshToken.EndDate);
        HttpContext.SaveUsernameCookie(req.Username, dto.RefreshToken.EndDate);

        await SendOkAsync("Welcome back!").ConfigureAwait(false);
    }
}
