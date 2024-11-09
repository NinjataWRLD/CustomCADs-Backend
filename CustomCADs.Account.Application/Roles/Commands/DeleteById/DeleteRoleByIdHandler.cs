using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Application.Events;

// will fix, pls just give me a sec
using RoleDeletedDomainEvent = CustomCADs.Account.Domain.DomainEvents.Roles.RoleDeletedEvent;
using RoleDeletedIntegrationEvent = CustomCADs.Shared.IntegrationEvents.Account.RoleDeletedEvent;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteById;

public class DeleteRoleByIdHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteRoleByIdCommand>
{
    public async Task Handle(DeleteRoleByIdCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(req.Id);

        writes.Remove(role);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        RoleDeletedDomainEvent rdDomainEvent = new(role.Id, role.Name);
        await raiser.RaiseAsync(rdDomainEvent).ConfigureAwait(false);

        RoleDeletedIntegrationEvent rdIntegrationEvent = new(role.Name);
        await raiser.RaiseAsync(rdIntegrationEvent).ConfigureAwait(false);
    }
}
