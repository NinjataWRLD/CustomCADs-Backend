using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Delete;

public sealed class DeleteRoleHandler(IRoleReads reads, IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteRoleCommand>
{
    public async Task Handle(DeleteRoleCommand req, CancellationToken ct)
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
