﻿using CustomCADs.Auth.Business.Contracts;
using CustomCADs.Auth.Data.Entities;
using CustomCADs.Auth.Endpoints.Helpers;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using static CustomCADs.Auth.Data.AuthConstants;

namespace CustomCADs.Auth.Endpoints.Auth.Login;

using static ApiMessages;
using static StatusCodes;

public class LoginEndpoint(
    IUserManager userManager,
    ITokenManager tokenManager,
    ISignInManager signInManager) : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("Login");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        AppUser? user = await userManager.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user == null || !user.EmailConfirmed)
        {
            ValidationFailures.Add(new(nameof(InvalidAccountOrEmail), InvalidAccountOrEmail));

            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        bool isLockedOut = await userManager.IsLockedOutAsync(user).ConfigureAwait(false);
        if (isLockedOut && user.LockoutEnd.HasValue)
        {
            TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            await SendLockedOutAsync(timeLeft).ConfigureAwait(false);
            return;
        }

        SignInResult result = await signInManager
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

            ValidationFailures.Add(new()
            {
                ErrorMessage = InvalidLogin,
            });
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        string role = await userManager.GetRoleAsync(user).ConfigureAwait(false);
        JwtSecurityToken jwt = tokenManager.GenerateAccessToken(user.Id, req.Username, role);

        string signedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        SaveJwt(signedJwt, jwt.ValidTo);

        string rt = tokenManager.GenerateRefreshToken();
        DateTime rtEndDate = DateTime.UtcNow.AddDays(RtDurationInDays);

        await userManager.UpdateRefreshTokenAsync(user.Id, rt, rtEndDate).ConfigureAwait(false);
        SaveRt(rt, rtEndDate);

        SaveRole(req.Username, rtEndDate);
        SaveUsername(role, rtEndDate);

        await SendOkAsync("Welcome back!").ConfigureAwait(false);
    }

    private async Task SendLockedOutAsync(TimeSpan timeLeft)
    {
        int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
        ValidationFailures.Add(new()
        {
            ErrorMessage = LockedOutUser,
            FormattedMessagePlaceholderValues = new()
            {
                ["seconds"] = seconds
            },
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
