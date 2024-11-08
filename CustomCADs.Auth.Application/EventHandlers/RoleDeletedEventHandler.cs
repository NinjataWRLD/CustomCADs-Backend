using CustomCADs.Shared.IntegrationEvents.Account;

namespace CustomCADs.Auth.Application.EventHandlers;

public class RoleDeletedEventHandler(IRoleService service)
{
    public async Task Handle(RoleDeletedEvent rdEvent)
    {
        AppRole role = await service.FindByNameAsync(rdEvent.Name).ConfigureAwait(false)
            ?? throw new RoleNotFoundException(rdEvent.Name, new { });

        await service.DeleteAsync(role).ConfigureAwait(false);
    }
}
