using CustomCADs.Accounts.Application.Roles.Queries;
using CustomCADs.Accounts.Application.Roles.Queries.GetAll;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Domain.Roles.Reads;

namespace CustomCADs.Accounts.Application.Roles.Queries.GetAll;

public class GetAllRolesHandler(IRoleReads reads)
    : IQueryHandler<GetAllRolesQuery, IEnumerable<RoleReadDto>>
{
    public async Task<IEnumerable<RoleReadDto>> Handle(GetAllRolesQuery req, CancellationToken ct)
    {
        IEnumerable<Role> roles = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        return roles.Select(r => new RoleReadDto(r.Id, r.Name, r.Description));
    }
}
