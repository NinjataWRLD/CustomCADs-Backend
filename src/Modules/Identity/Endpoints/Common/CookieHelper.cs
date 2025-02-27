namespace CustomCADs.Identity.Endpoints.Common;

public static class CookieHelper
{
    private const string AccessTokenCookie = "jwt";
    private const string RefreshTokenCookie = "rt";
    private const string RoleCookie = "role";
    private const string UsernameCookie = "username";

    public static string? GetRefreshTokenCookie(this HttpContext context)
        => context.Request.Cookies.FirstOrDefault(c => c.Key == RefreshTokenCookie).Value;

    public static void SaveAccessTokenCookie(this HttpContext context, string jwt, DateTime expire)
        => context.Response.Cookies.Append(AccessTokenCookie, jwt, HttpOnlyCookieOptions(expire));

    public static void SaveRefreshTokenCookie(this HttpContext context, string rt, DateTime expire)
        => context.Response.Cookies.Append(RefreshTokenCookie, rt, HttpOnlyCookieOptions(expire));

    public static void SaveUsernameCookie(this HttpContext context, string username, DateTime expire)
        => context.Response.Cookies.Append(UsernameCookie, username, CookieOptions(expire));

    public static void SaveRoleCookie(this HttpContext context, string role, DateTime expire)
        => context.Response.Cookies.Append(RoleCookie, role, CookieOptions(expire));

    public static void DeleteAllCookies(this HttpContext context)
    {
        string[] cookies = [
            AccessTokenCookie,
            RefreshTokenCookie,
            RoleCookie,
            UsernameCookie,
        ];

        foreach (string cookie in cookies)
        {
            context.Response.Cookies.Delete(cookie);
        }
    }

    private static CookieOptions CookieOptions(DateTime expire)
        => new()
        {
            Secure = true,
            Expires = expire,
            SameSite = SameSiteMode.None,
        };

    private static CookieOptions HttpOnlyCookieOptions(DateTime expire)
        => new()
        {
            HttpOnly = true,
            Secure = true,
            Expires = expire,
            SameSite = SameSiteMode.None,
        };
}