using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Endpoints.Common;

public static class CookieHelper
{
    private const string AccessTokenCookie = "jwt";
    private const string RefreshTokenCookie = "rt";
    private const string RoleCookie = "role";
    private const string UsernameCookie = "username";

    public static string? GetRefreshTokenCookie(this HttpContext context)
        => context.Request.Cookies.FirstOrDefault(c => c.Key == RefreshTokenCookie).Value;

    public static void SaveAccessTokenCookie(this HttpContext context, AccessTokenDto jwt, string? domain)
        => context.Response.Cookies.Append(AccessTokenCookie, jwt.Value, HttpOnlyCookieOptions(jwt.ExpiresAt, domain));

    public static void SaveRefreshTokenCookie(this HttpContext context, RefreshTokenDto rt, string? domain)
        => context.Response.Cookies.Append(RefreshTokenCookie, rt.Value, HttpOnlyCookieOptions(rt.ExpiresAt, domain));

    public static void SaveUsernameCookie(this HttpContext context, string username, DateTimeOffset expire, string? domain)
        => context.Response.Cookies.Append(UsernameCookie, username, CookieOptions(expire, domain));

    public static void SaveRoleCookie(this HttpContext context, string role, DateTimeOffset expire, string? domain)
        => context.Response.Cookies.Append(RoleCookie, role, CookieOptions(expire, domain));

    public static void SaveAllCookies(
        this HttpContext context,
        TokensDto tokens,
        string username,
        string? domain
    )
    {
        context.SaveAccessTokenCookie(tokens.AccessToken, domain);
        context.SaveRefreshTokenCookie(tokens.RefreshToken, domain);
        context.SaveRoleCookie(tokens.Role, tokens.RefreshToken.ExpiresAt, domain);
        context.SaveUsernameCookie(username, tokens.RefreshToken.ExpiresAt, domain);
    }

    public static void DeleteAllCookies(this HttpContext context, string? domain)
    {
        DateTimeOffset expiresAt = DateTimeOffset.UnixEpoch;

        context.Response.Cookies.Append(AccessTokenCookie, string.Empty, HttpOnlyCookieOptions(expiresAt, domain));
        context.Response.Cookies.Append(RefreshTokenCookie, string.Empty, HttpOnlyCookieOptions(expiresAt, domain));
        context.Response.Cookies.Append(RoleCookie, string.Empty, CookieOptions(expiresAt, domain));
        context.Response.Cookies.Append(UsernameCookie, string.Empty, CookieOptions(expiresAt, domain));
    }

    private static CookieOptions CookieOptions(DateTimeOffset expire, string? domain)
        => new()
        {
            Domain = domain,
            Secure = true,
            Expires = expire,
            SameSite = SameSiteMode.None,
        };

    private static CookieOptions HttpOnlyCookieOptions(DateTimeOffset expire, string? domain)
        => new()
        {
            Domain = domain,
            HttpOnly = true,
            Secure = true,
            Expires = expire,
            SameSite = SameSiteMode.None,
        };
}