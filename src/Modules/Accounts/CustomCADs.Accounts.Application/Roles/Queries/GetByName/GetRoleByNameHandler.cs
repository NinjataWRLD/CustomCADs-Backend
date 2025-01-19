using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Accounts.Application.Roles.Queries.GetByName;

public sealed class GetRoleByNameHandler(IRoleReads reads, ICacheService cache)
    : IQueryHandler<GetRoleByNameQuery, RoleReadDto>
{
    public async Task<RoleReadDto> Handle(GetRoleByNameQuery req, CancellationToken ct)
    {
        Role? role = await cache.GetRoleAsync(req.Name).ConfigureAwait(false);

        if (role is null)
        {
            role = await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
                ?? throw RoleNotFoundException.ByName(req.Name);

            await cache.SetRoleAsync(role.Id, role).ConfigureAwait(false);
            await cache.SetRoleAsync(role.Name, role).ConfigureAwait(false);
        }

        return role.ToRoleReadDto();
    }
}
