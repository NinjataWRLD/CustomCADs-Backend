using CustomCADs.Account.Domain.Roles.DomainEvents;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Account.Application.Roles.Commands.Create;

public class CreateRoleHandler(IWrites<Role> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateRoleCommand, int>
{
    public async Task<int> Handle(CreateRoleCommand req, CancellationToken ct)
    {
        Role role = new()
        {
            Name = req.Dto.Name,
            Description = req.Dto.Description,
        };

        await writes.AddAsync(role, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseAsync(new RoleCreatedDomainEvent(
            Role: role
        )).ConfigureAwait(false);

        await raiser.RaiseAsync(new RoleCreatedIntegrationEvent(
            Name: req.Dto.Name,
            Description: req.Dto.Description
        )).ConfigureAwait(false);

        return role.Id;
    }
}
