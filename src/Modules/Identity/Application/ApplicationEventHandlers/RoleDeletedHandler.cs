using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class RoleDeletedHandler(IRoleService service)
{
    public async Task Handle(RoleDeletedApplicationEvent ae)
    {
        AppRole role = await service.FindByNameAsync(ae.Name).ConfigureAwait(false)
            ?? throw RoleNotFoundException.ByName(ae.Name);

        await service.DeleteAsync(role).ConfigureAwait(false);
    }
}
