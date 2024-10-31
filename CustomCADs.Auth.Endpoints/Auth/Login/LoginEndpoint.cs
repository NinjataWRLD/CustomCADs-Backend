using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Endpoints.Helpers;
using CustomCADs.Auth.Infrastructure.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using static CustomCADs.Auth.Infrastructure.AuthConstants;

namespace CustomCADs.Auth.Endpoints.Auth.Login;

using static ApiMessages;
using static StatusCodes;

public class LoginEndpoint(
    IUserService userService,
    ITokenService tokenService,
    ISignInService signInService) : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("Login");
        Group<AuthGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        AppUser? user = await userService.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user == null || !user.EmailConfirmed)
        {
            ValidationFailures.Add(new(nameof(InvalidAccountOrEmail), InvalidAccountOrEmail));

            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        bool isLockedOut = await userService.IsLockedOutAsync(user).ConfigureAwait(false);
        if (isLockedOut && user.LockoutEnd.HasValue)
        {
            TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            await SendLockedOutAsync(timeLeft).ConfigureAwait(false);
            return;
        }

        SignInResult result = await signInService
            .PasswordSignInAsync(user, req.Password, req.RememberMe ?? false, lockoutOnFailure: true)
            .ConfigureAwait(false);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut && user.LockoutEnd.HasValue)
            {
                TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
                await SendLockedOutAsync(timeLeft).ConfigureAwait(false);
                return;
            }

            ValidationFailures.Add(new("Password", InvalidLogin, req.Password));
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.Id, req.Username, role);
        SaveJwt(jwt.Value, jwt.EndDate);

        string rt = tokenService.GenerateRefreshToken();
        DateTime rtEndDate = DateTime.UtcNow.AddDays(RtDurationInDays);

        await userService.UpdateRefreshTokenAsync(user.Id, rt, rtEndDate).ConfigureAwait(false);
        SaveRt(rt, rtEndDate);

        SaveRole(role, rtEndDate);
        SaveUsername(req.Username, rtEndDate);

        await SendOkAsync("Welcome back!").ConfigureAwait(false);
    }

    private async Task SendLockedOutAsync(TimeSpan timeLeft)
    {
        int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
        ValidationFailures.Add(new("Password", LockedOutUser)
        {
            FormattedMessagePlaceholderValues = new() { ["seconds"] = seconds },
            CustomState = seconds,
        });

        await SendErrorsAsync(Status423Locked).ConfigureAwait(false);
    }

    private void SaveJwt(string jwt, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("jwt", jwt, new()
        {
            HttpOnly = true,
            Secure = true,
            Expires = expire,
        });
    }

    private void SaveRt(string rt, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("rt", rt, new()
        {
            HttpOnly = true,
            Secure = true,
            Expires = expire,
        });
    }

    private void SaveRole(string role, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("role", role, new() { Expires = expire });
    }

    private void SaveUsername(string username, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("username", username, new() { Expires = expire });
    }
}
