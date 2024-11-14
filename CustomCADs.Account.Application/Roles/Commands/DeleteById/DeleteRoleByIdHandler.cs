using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Roles.DomainEvents;
using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteById;

public class DeleteRoleByIdHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteRoleByIdCommand>
{
    public async Task Handle(DeleteRoleByIdCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ById(req.Id);

        writes.Remove(role);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseAsync(new RoleDeletedDomainEvent(
            Id: role.Id,
            Name: role.Name
        )).ConfigureAwait(false);

        await raiser.RaiseAsync(new RoleDeletedIntegrationEvent(
            Name: role.Name
        )).ConfigureAwait(false);
    }
}
