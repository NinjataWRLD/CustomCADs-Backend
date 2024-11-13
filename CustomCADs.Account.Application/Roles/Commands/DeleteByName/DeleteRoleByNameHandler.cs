using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Roles.DomainEvents;
using CustomCADs.Account.Domain.Roles.Entities;
using CustomCADs.Account.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteByName;

public class DeleteRoleByNameHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteRoleByNameCommand>
{
    public async Task Handle(DeleteRoleByNameCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(req.Name);

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
