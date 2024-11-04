using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Infrastructure.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using static CustomCADs.Auth.Infrastructure.AuthConstants;

namespace CustomCADs.Auth.Endpoints.SignIn.RefreshToken;

using static Helpers.ApiMessages;
using static StatusCodes;

public class RefreshTokenEndpoint(IUserService userService, ITokenService tokenService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("refreshToken");
        Group<SignInGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string? rt = GetRefreshTokenFromCookies();
        if (string.IsNullOrEmpty(rt))
        {
            ValidationFailures.Add(new("RefreshToken", NoRefreshToken));
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        AppUser? user = await userService.FindByRefreshTokenAsync(rt).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("RefreshToken", UserNotFound, rt));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        if (user.RefreshTokenEndDate < DateTime.UtcNow)
        {
            DeleteCookies("jwt", "rt", "username", "role");

            ValidationFailures.Add(new("RefreshToken", RefreshTokenExpired, rt));
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto newJwt = tokenService.GenerateAccessToken(user.Id, user.UserName ?? string.Empty, role);
        SaveAccessToken(newJwt.Value, newJwt.EndDate);

        if (user.RefreshTokenEndDate >= DateTime.UtcNow.AddMinutes(1))
        {
            await SendOkAsync(NewRefreshTokenNotNeeded).ConfigureAwait(false);
            return;
        }

        string newRt = tokenService.GenerateRefreshToken();
        DateTime newRtEnd = DateTime.UtcNow.AddDays(RtDurationInDays);
        await userService.UpdateRefreshTokenAsync(user.Id, newRt, newRtEnd).ConfigureAwait(false);

        SaveRefreshToken(newRt, newRtEnd);

        SaveRole(role, newRtEnd);
        SaveUsername(user.UserName ?? string.Empty, newRtEnd);

        await SendOkAsync(NewRefreshTokenGranted).ConfigureAwait(false);
    }

    private void SaveAccessToken(string jwt, DateTime end)
    {
        HttpContext.Response.Cookies.Append("jwt", jwt, new() { HttpOnly = true, Secure = true, Expires = end });
    }

    private void SaveRefreshToken(string rt, DateTime end)
    {
        HttpContext.Response.Cookies.Append("rt", rt, new() { HttpOnly = true, Secure = true, Expires = end });
    }

    private void SaveUsername(string username, DateTime end)
    {
        HttpContext.Response.Cookies.Append("username", username, new() { Expires = end });
    }

    private void SaveRole(string role, DateTime end)
    {
        HttpContext.Response.Cookies.Append("role", role, new() { Expires = end });
    }

    private void DeleteCookies(params string[] cookies)
    {
        foreach (string cookie in cookies)
        {
            HttpContext.Response.Cookies.Delete(cookie);
        }
    }

    private string? GetRefreshTokenFromCookies()
    {
        return HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "rt").Value;
    }
}
