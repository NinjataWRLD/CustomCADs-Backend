using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.IntegrationEventHandlers;

public class RoleEditedEventHandler(IRoleService service)
{
    public async Task Handle(RoleEditedIntegrationEvent ie)
    {
        AppRole role = await service.FindByNameAsync(ie.Name).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(ie.Name);

        role.Name = ie.Name;

        await service.UpdateAsync(role).ConfigureAwait(false);
    }
}
