using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.Identity.Application.Users.Events.Application.Roles;

public class RoleDeletedHandler(IRoleWrites writes)
{
	public async Task Handle(RoleDeletedApplicationEvent ae)
		=> await writes.DeleteAsync(
			name: ae.Name
		).ConfigureAwait(false);
}
