using CustomCADs.Account.Domain.Common.Exceptions.Roles;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public class GetRoleByIdHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetRoleByIdQuery, RoleReadDto>
{
    public async Task<RoleReadDto> Handle(GetRoleByIdQuery req, CancellationToken ct)
    {
        Role role =
            await cache.GetAsync<Role>($"roles/{req.Id}").ConfigureAwait(false)
            ?? await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ById(req.Id);

        return new(role.Id, role.Name, role.Description);
    }
}
