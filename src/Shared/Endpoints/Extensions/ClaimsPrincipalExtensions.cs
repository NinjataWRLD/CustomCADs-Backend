using CustomCADs.Shared.Domain.TypedIds.Accounts;
using System.Security.Claims;

namespace CustomCADs.Shared.Endpoints.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static AccountId GetAccountId(this ClaimsPrincipal user)
		=> AccountId.New(user.FindFirstValue(ClaimTypes.NameIdentifier));

	public static string GetName(this ClaimsPrincipal user)
		=> user.Identity?.Name ?? string.Empty;

	public static bool GetAuthentication(this ClaimsPrincipal user)
		=> user.Identity?.IsAuthenticated ?? false;

	public static string? GetAuthorization(this ClaimsPrincipal user)
		=> user.FindFirstValue(ClaimTypes.Role);
}

