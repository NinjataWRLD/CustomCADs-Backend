using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class RoleCreatedHandler(IRoleService service)
{
    public async Task Handle(RoleCreatedApplicationEvent ae)
    {
        AppRole role = new(ae.Name);
        await service.CreateAsync(role).ConfigureAwait(false);
    }
}
