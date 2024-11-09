using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.Queries.GetAllNames;

public class GetAllRoleNamesHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetAllRoleNamesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetAllRoleNamesQuery req, CancellationToken ct)
    {
        IEnumerable<Role> roles =
            await cache.GetAsync<IEnumerable<Role>>("roleNames").ConfigureAwait(false)
            ?? await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        var response = roles.Select(r => r.Name);
        return response;
    }
}
