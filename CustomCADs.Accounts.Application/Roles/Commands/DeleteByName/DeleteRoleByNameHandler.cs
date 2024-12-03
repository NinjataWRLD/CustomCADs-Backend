using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.DeleteByName;

public sealed class DeleteRoleByNameHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteRoleByNameCommand>
{
    public async Task Handle(DeleteRoleByNameCommand req, CancellationToken ct)
    {
        Role role = await reads.SingleByNameAsync(req.Name, ct: ct).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(req.Name);

        writes.Remove(role);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new RoleDeletedDomainEvent(
            Id: role.Id,
            Name: role.Name
        )).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new RoleDeletedIntegrationEvent(
            Name: role.Name
        )).ConfigureAwait(false);
    }
}
