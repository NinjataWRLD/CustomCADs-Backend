using System.Security.Claims;

namespace CustomCADs.Shared.Presentation;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal user)
    {
        string id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        bool validId = Guid.TryParse(id, out Guid guid);
        if (!validId) 
        {
            throw new Exception("Cannot find User ID.");
        }

        return guid;
    }

    public static string GetName(this ClaimsPrincipal user)
        => user.Identity?.Name ?? string.Empty;

    public static bool GetAuthentication(this ClaimsPrincipal user)
        => user.Identity?.IsAuthenticated ?? false;

    public static string GetAuthorization(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
}
