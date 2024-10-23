using CustomCADs.Account.Application.Roles;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using Mapster;

namespace CustomCADs.Account.Application.Roles.Queries.GetAll;

public class GetAllRolesHandler(IRoleReads reads)
{
    public async Task<IEnumerable<RoleReadDto>> Handle(GetAllRolesQuery req, CancellationToken ct)
    {
        IEnumerable<Role> roles = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        var response = roles.Adapt<IEnumerable<RoleReadDto>>();
        return response;
    }
}
