using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Accounts.Application.Roles.Queries.GetByName;

public class GetRoleByNameHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetRoleByNameQuery, RoleReadDto>
{
    public async Task<RoleReadDto> Handle(GetRoleByNameQuery req, CancellationToken ct)
    {
        Role role =
            await cache.GetAsync<Role>($"roles/{req.Name}").ConfigureAwait(false)
            ?? await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(req.Name);

        return role.ToRoleReadDto();
    }
}
