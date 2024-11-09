using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Application.Events;

// will fix, just give me a sec
using RoleCreatedDomainEvent = CustomCADs.Account.Domain.DomainEvents.Roles.RoleCreatedEvent;
using RoleCreatedIntegrationEvent = CustomCADs.Shared.IntegrationEvents.Account.RoleCreatedEvent;

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

        RoleCreatedDomainEvent rcDomainEvent = new(role);
        await raiser.RaiseAsync(rcDomainEvent).ConfigureAwait(false);

        RoleCreatedIntegrationEvent rcIntegrationEvent = new(req.Dto.Name, req.Dto.Description);
        await raiser.RaiseAsync(rcIntegrationEvent).ConfigureAwait(false);
        
        return role.Id;
    }
}
