using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Endpoints.Common;

public static class CookieHelper
{
    private const string AccessTokenCookie = "jwt";
    private const string RefreshTokenCookie = "rt";
    private const string CsrfTokenCookie = "csrf";
    private const string RoleCookie = "role";
    private const string UsernameCookie = "username";

    public static string? GetRefreshTokenCookie(this HttpContext context)
        => context.Request.Cookies.FirstOrDefault(c => c.Key == RefreshTokenCookie).Value;

    public static void SaveAccessTokenCookie(this HttpContext context, TokenDto jwt, string? domain)
        => context.Response.Cookies.Append(AccessTokenCookie, jwt.Value, CookieOptions(jwt.ExpiresAt, httpOnly: true, domain: domain));

    public static void SaveRefreshTokenCookie(this HttpContext context, TokenDto rt, string? domain)
        => context.Response.Cookies.Append(RefreshTokenCookie, rt.Value, CookieOptions(rt.ExpiresAt, httpOnly: true, domain: domain));

    public static void SaveCsrfTokenCookie(this HttpContext context, TokenDto csrf, string? domain)
        => context.Response.Cookies.Append(CsrfTokenCookie, csrf.Value, CookieOptions(csrf.ExpiresAt, domain: domain));

    public static void SaveUsernameCookie(this HttpContext context, string username, DateTimeOffset expire, string? domain)
        => context.Response.Cookies.Append(UsernameCookie, username, CookieOptions(expire, domain: domain));

    public static void SaveRoleCookie(this HttpContext context, string role, DateTimeOffset expire, string? domain)
        => context.Response.Cookies.Append(RoleCookie, role, CookieOptions(expire, domain: domain));

    public static void SaveAllCookies(
        this HttpContext context,
        TokensDto tokens,
        string username,
        string? domain
    )
    {
        context.SaveAccessTokenCookie(tokens.AccessToken, domain);
        context.SaveCsrfTokenCookie(tokens.CsrfToken, domain);
        context.SaveRefreshTokenCookie(tokens.RefreshToken, domain);
        context.SaveRoleCookie(tokens.Role, tokens.RefreshToken.ExpiresAt, domain);
        context.SaveUsernameCookie(username, tokens.RefreshToken.ExpiresAt, domain);
    }

    public static void DeleteAllCookies(this HttpContext context, string? domain)
    {
        void DeleteCookie(string key, bool httpOnly = false, string? domain = null)
        {
            context.Response.Cookies.Append(
                key: key,
                value: string.Empty,
                options: CookieOptions(DateTimeOffset.UnixEpoch, httpOnly, domain)
            );
        }

        DeleteCookie(AccessTokenCookie, httpOnly: true, domain: domain);
        DeleteCookie(RefreshTokenCookie, httpOnly: true, domain: domain);
        DeleteCookie(CsrfTokenCookie, domain: domain);
        DeleteCookie(RoleCookie, domain: domain);
        DeleteCookie(UsernameCookie, domain: domain);
    }

    private static CookieOptions CookieOptions(DateTimeOffset expire, bool httpOnly = false, string? domain = null)
        => new()
        {
            HttpOnly = httpOnly,
            Domain = domain,
            Secure = true,
            Expires = expire,
            SameSite = SameSiteMode.None,
        };
}