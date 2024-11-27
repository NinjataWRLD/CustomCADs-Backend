using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Auth.Application.IntegrationEventHandlers;

public class RoleCreatedEventHandler(IRoleService service)
{
    public async Task Handle(RoleCreatedIntegrationEvent ie)
    {
        AppRole role = new() { Name = ie.Name };
        await service.CreateAsync(role).ConfigureAwait(false);
    }
}
