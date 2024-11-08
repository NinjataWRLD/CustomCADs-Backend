using CustomCADs.Shared.IntegrationEvents.Account;

namespace CustomCADs.Auth.Application.EventHandlers;

public class RoleCreatedEventHandler(IRoleService service)
{
    public async Task Handle(RoleCreatedEvent rcEvent)
    {
        AppRole role = new() { Name = rcEvent.Name };
        await service.CreateAsync(role).ConfigureAwait(false);
    }
}
