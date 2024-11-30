using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Create;

public class CreateRoleHandler(IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateRoleCommand, RoleId>
{
    public async Task<RoleId> Handle(CreateRoleCommand req, CancellationToken ct)
    {
        var role = Role.Create(req.Dto.Name, req.Dto.Description);

        await writes.AddAsync(role, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new RoleCreatedDomainEvent(
            Role: role
        )).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new RoleCreatedIntegrationEvent(
            Name: req.Dto.Name,
            Description: req.Dto.Description
        )).ConfigureAwait(false);

        return role.Id;
    }
}
