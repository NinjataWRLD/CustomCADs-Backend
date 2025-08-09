using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.Users.Events.Application.Roles;

public class RoleDeletedHandler(IRoleService service)
{
	public async Task Handle(RoleDeletedApplicationEvent ae)
		=> await service.DeleteAsync(
			name: ae.Name
		).ConfigureAwait(false);
}
