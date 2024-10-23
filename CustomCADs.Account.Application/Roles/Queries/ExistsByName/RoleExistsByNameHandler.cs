using CustomCADs.Account.Domain.Roles.Reads;

namespace CustomCADs.Account.Application.Roles.Queries.ExistsByName;

public class RoleExistsByNameHandler(IRoleReads reads)
{
    public async Task<bool> Handle(RoleExistsByNameQuery req, CancellationToken ct)
    {
        bool roleExists = await reads.ExistsByNameAsync(req.Name, ct: ct).ConfigureAwait(false);

        return roleExists;
    }
}
