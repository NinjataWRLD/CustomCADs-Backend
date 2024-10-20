using System.Security.Claims;

namespace CustomCADs.Auth.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal User) => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
    public static string GetName(this ClaimsPrincipal User) => User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    public static bool GetIsAuthenticated(this ClaimsPrincipal User) => User.Identity?.IsAuthenticated ?? false;
}
