using CustomCADs.Account.Application.Roles.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using Mapster;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public class GetRoleByIdHandler(IRoleReads reads)
{
    public async Task<RoleReadDto> Handle(GetRoleByIdQuery req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException($"The Role with id: {req.Id} does not exist.");

        var response = role.Adapt<RoleReadDto>();
        return response;
    }
}
