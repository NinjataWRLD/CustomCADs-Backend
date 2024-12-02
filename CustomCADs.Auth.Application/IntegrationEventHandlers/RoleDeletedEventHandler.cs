using CustomCADs.Auth.Application.Common.Exceptions;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Auth.Application.IntegrationEventHandlers;

public class RoleDeletedEventHandler(IRoleService service)
{
    public async Task Handle(RoleDeletedIntegrationEvent ie)
    {
        AppRole role = await service.FindByNameAsync(ie.Name).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(ie.Name);

        await service.DeleteAsync(role).ConfigureAwait(false);
    }
}
