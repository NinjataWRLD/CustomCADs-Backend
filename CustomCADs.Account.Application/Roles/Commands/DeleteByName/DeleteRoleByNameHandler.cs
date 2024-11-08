using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Core.Events;
using CustomCADs.Shared.IntegrationEvents.Account;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteByName;

public class DeleteRoleByNameHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteRoleByNameCommand>
{
    public async Task Handle(DeleteRoleByNameCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Name, new { });

        writes.Remove(role);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        RoleDeletedEvent rdEvent = new(req.Name);
        await raiser.PublishAsync(rdEvent).ConfigureAwait(false);
    }
}
