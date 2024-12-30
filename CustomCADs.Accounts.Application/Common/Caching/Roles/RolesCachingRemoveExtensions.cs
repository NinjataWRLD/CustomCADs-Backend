using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Accounts.Application.Common.Caching.Roles;

using static CachingKeys;

public static class RolesCachingRemoveExtensions
{
    public static async Task RemoveRolesArrayAsync(this ICacheService cache)
        => await cache
            .RemoveAsync<IEnumerable<Role>>(RoleKey)
            .ConfigureAwait(false);

    public static async Task RemoveRoleAsync(this ICacheService cache, RoleId id)
        => await cache
            .RemoveAsync<Role>($"{RoleKey}/{id}")
            .ConfigureAwait(false);

    public static async Task RemoveRoleAsync(this ICacheService cache, string name)
        => await cache
            .RemoveAsync<Role>($"{RoleKey}/{name}")
            .ConfigureAwait(false);
}
