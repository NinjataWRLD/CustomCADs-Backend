using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Infrastructure.Identity;

public static class Extensions
{
	public static async Task<User> ToUserWithRoleAsync(this AppUser appUser, UserManager<AppUser> manager)
		=> appUser.ToUser(role: await manager.GetRoleAsync(appUser).ConfigureAwait(false));

	public static async Task<(User User, RefreshToken Token)> ToUserWithRoleAsync(this AppUser appUser, UserManager<AppUser> manager, string token)
		=> (
			User: appUser.ToUser(role: await manager.GetRoleAsync(appUser).ConfigureAwait(false)),
			Token: appUser.RefreshTokens.First(x => x.Value == token).ToRefreshToken()
		);

	private static async Task<string> GetRoleAsync(this UserManager<AppUser> manager, AppUser appUser)
	{
		string[] roles = [.. await manager.GetRolesAsync(appUser).ConfigureAwait(false)];
		return roles.Single();
	}
}
