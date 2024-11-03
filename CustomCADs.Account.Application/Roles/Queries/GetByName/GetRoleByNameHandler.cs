using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Application.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;

namespace CustomCADs.Account.Application.Roles.Queries.GetByName;

public class GetRoleByNameHandler(IRoleReads reads)
    : IQueryHandler<GetRoleByNameQuery, RoleReadDto>
{
    public async Task<RoleReadDto> Handle(GetRoleByNameQuery req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, track: false, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Name, new { });

        RoleReadDto response = new(role.Id, role.Name, role.Description);
        return response;
    }
}
