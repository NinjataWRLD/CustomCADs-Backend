using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.Users.Events.Application;

public class RoleDeletedHandler(IRoleManager manager)
{
    public async Task Handle(RoleDeletedApplicationEvent ae)
        => await manager.DeleteAsync(
            name: ae.Name
        ).ConfigureAwait(false);
}
