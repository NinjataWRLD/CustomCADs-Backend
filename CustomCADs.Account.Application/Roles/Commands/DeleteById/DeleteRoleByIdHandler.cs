using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Application.Common.Exceptions;
using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteById;

public class DeleteRoleByIdHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteRoleByIdCommand>
{
    public async Task Handle(DeleteRoleByIdCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Id);

        writes.Remove(role);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
