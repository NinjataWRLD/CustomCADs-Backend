using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class RoleEditedHandler(IRoleService service)
{
    public async Task Handle(RoleEditedApplicationEvent ae)
    {
        AppRole role = await service.FindByNameAsync(ae.Name).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(ae.Name);

        role.Name = ae.Name;

        await service.UpdateAsync(role).ConfigureAwait(false);
    }
}
