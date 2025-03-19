using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Accounts.Application.Roles.Commands.Create;

public sealed class CreateRoleHandler(IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
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
