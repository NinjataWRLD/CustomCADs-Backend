using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Common.Caching.Roles;

using static CachingKeys;

public static class RolesCachingGetExtensions
{
	public static async Task<IEnumerable<Role>?> GetRolesArrayAsync(this ICacheService cache)
		=> await cache
			.GetAsync<IEnumerable<Role>>(RoleKey)
			.ConfigureAwait(false);

	public static async Task<Role?> GetRoleAsync(this ICacheService cache, RoleId id)
		=> await cache
			.GetAsync<Role>($"{RoleKey}/{id}")
			.ConfigureAwait(false);

	public static async Task<Role?> GetRoleAsync(this ICacheService cache, string name)
		=> await cache
			.GetAsync<Role>($"{RoleKey}/{name}")
			.ConfigureAwait(false);
}
