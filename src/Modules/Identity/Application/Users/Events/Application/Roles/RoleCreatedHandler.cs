using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.Users.Events.Application.Roles;

public class RoleCreatedHandler(IRoleService service)
{
	public async Task Handle(RoleCreatedApplicationEvent ae)
		=> await service.CreateAsync(
			name: ae.Name
		).ConfigureAwait(false);
}
