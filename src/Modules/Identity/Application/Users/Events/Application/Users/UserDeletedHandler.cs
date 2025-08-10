using CustomCADs.Shared.Application.Events.Account.Accounts;

namespace CustomCADs.Identity.Application.Users.Events.Application.Users;

public class UserDeletedHandler(IUserService service)
{
	public async Task Handle(AccountDeletedApplicationEvent ae)
	{
		await service.DeleteAsync(ae.Username).ConfigureAwait(false);
	}
}
