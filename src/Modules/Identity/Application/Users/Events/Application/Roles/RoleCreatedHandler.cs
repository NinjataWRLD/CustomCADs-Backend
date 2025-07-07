using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.Users.Events.Application.Roles;

public class RoleCreatedHandler(IRoleWrites writes)
{
	public async Task Handle(RoleCreatedApplicationEvent ae)
		=> await writes.CreateAsync(
			name: ae.Name
		).ConfigureAwait(false);
}
