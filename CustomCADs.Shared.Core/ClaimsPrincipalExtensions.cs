using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using System.Security.Claims;

namespace CustomCADs.Shared.Core;

public static class ClaimsPrincipalExtensions
{
    public static AccountId GetAccountId(this ClaimsPrincipal user)
        => new((user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty).ToGuid());

    public static string GetName(this ClaimsPrincipal user)
        => user.Identity?.Name ?? string.Empty;

    public static bool GetAuthentication(this ClaimsPrincipal user)
        => user.Identity?.IsAuthenticated ?? false;

    public static string? GetAuthorization(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.Role)?.Value;

    private static Guid ToGuid(this string str)
        => Guid.TryParse(str, out Guid guid) ? guid : Guid.Empty;
}

