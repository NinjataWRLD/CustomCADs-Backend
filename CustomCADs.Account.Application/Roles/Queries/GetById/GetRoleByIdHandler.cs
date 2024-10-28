using CustomCADs.Account.Application.Roles.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public class GetRoleByIdHandler(IRoleReads reads)
{
    public async Task<RoleReadDto> Handle(GetRoleByIdQuery req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Id);

        RoleReadDto response = new(role.Id, role.Name, role.Description);
        return response;
    }
}
