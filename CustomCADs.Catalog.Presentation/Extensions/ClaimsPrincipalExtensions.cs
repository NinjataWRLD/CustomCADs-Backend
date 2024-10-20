using System.Security.Claims;

namespace CustomCADs.Catalog.Presentation.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal user) => Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
    public static string GetName(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    public static bool GetIsAuthenticated(this ClaimsPrincipal user) => user.Identity?.IsAuthenticated ?? false;
}