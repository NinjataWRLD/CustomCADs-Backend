using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Common.Caching.Roles;

using static CachingKeys;

public static class RolesCachingSetExtensions
{
    public static async Task SetRolesArrayAsync(this ICacheService cache, params IEnumerable<Role> roles)
        => await cache
            .SetAsync(RoleKey, roles)
            .ConfigureAwait(false);

    public static async Task SetRoleAsync(this ICacheService cache, RoleId id, Role role)
        => await cache
            .SetAsync($"{RoleKey}/{id}", role)
            .ConfigureAwait(false);

    public static async Task SetRoleAsync(this ICacheService cache, string name, Role role)
        => await cache
            .SetAsync($"{RoleKey}/{name}", role)
            .ConfigureAwait(false);
}
