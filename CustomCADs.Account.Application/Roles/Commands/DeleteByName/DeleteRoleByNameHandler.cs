using CustomCADs.Account.Application.Roles.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteByName;

public class DeleteRoleByNameHandler(
    IRoleReads reads,
    IWrites<Role> writes,
    IUnitOfWork uow)
{
    public async Task Handle(DeleteRoleByNameCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Name, new { });

        writes.Remove(role);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
