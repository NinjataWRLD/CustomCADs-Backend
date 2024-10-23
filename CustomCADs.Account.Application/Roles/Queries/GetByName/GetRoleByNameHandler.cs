using CustomCADs.Account.Application.Roles.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using Mapster;

namespace CustomCADs.Account.Application.Roles.Queries.GetByName;

public class GetRoleByNameHandler(IRoleReads reads)
{
    public async Task<RoleReadDto> Handle(GetRoleByNameQuery req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException($"The Role with name: {req.Name} does not exist.");

        var response = role.Adapt<RoleReadDto>();
        return response;
    }
}
