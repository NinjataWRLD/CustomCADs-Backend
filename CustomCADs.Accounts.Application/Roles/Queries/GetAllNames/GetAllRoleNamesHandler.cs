using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Accounts.Application.Roles.Queries.GetAllNames;

public sealed class GetAllRoleNamesHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetAllRoleNamesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetAllRoleNamesQuery req, CancellationToken ct)
    {
        IEnumerable<Role> roles =
            await cache.GetAsync<IEnumerable<Role>>("roleNames").ConfigureAwait(false)
            ?? await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        return roles.Select(r => r.Name);
    }
}
