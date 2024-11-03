using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Domain.Roles.Reads;

namespace CustomCADs.Account.Application.Roles.Queries.ExistsById;

public class RoleExistsByIdHandler(IRoleReads reads)
    : IQueryHandler<RoleExistsByIdQuery, bool>
{
    public async Task<bool> Handle(RoleExistsByIdQuery req, CancellationToken ct)
    {
        bool roleExists = await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);

        return roleExists;
    }
}
