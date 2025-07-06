using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.Users.Events.Application.Roles;

public class RoleCreatedHandler(IRoleManager manager)
{
	public async Task Handle(RoleCreatedApplicationEvent ae)
		=> await manager.CreateAsync(
			name: ae.Name
		).ConfigureAwait(false);
}
