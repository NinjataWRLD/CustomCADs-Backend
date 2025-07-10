using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.Users.Events.Application.Users;

public class UserDeletedHandler(IUserWrites writes)
{
	public async Task Handle(AccountDeletedApplicationEvent ae)
	{
		await writes.DeleteAsync(ae.Username).ConfigureAwait(false);
	}
}
