using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Auth.Application.EventHandlers;

public class RoleDeletedEventHandler(IRoleService service)
{
    public async Task Handle(RoleDeletedIntegrationEvent ie)
    {
        AppRole role = await service.FindByNameAsync(ie.Name).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(ie.Name, new { });

        await service.DeleteAsync(role).ConfigureAwait(false);
    }
}
