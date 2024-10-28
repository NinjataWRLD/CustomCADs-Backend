using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;

namespace CustomCADs.Account.Application.Roles.Queries.GetAll;

public class GetAllRolesHandler(IRoleReads reads)
{
    public async Task<IEnumerable<RoleReadDto>> Handle(GetAllRolesQuery req, CancellationToken ct)
    {
        IEnumerable<Role> roles = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        var response = roles.Select(r => new RoleReadDto(r.Id, r.Name, r.Description));
        return response;
    }
}
