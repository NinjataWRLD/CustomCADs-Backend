using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.Queries.GetByName;

public class GetRoleByNameHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetRoleByNameQuery, RoleReadDto>
{
    public async Task<RoleReadDto> Handle(GetRoleByNameQuery req, CancellationToken ct)
    {
        Role role =
            await cache.GetAsync<Role>($"roles/{req.Name}").ConfigureAwait(false)
            ?? await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(req.Name);

        return new(role.Id, role.Name, role.Description);
    }
}
