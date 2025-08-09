using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.Users.Events.Application.Users;

public class UserCreatedHandler(IUserService service)
{
	public async Task Handle(AccountCreatedApplicationEvent ae)
	{
		await service.CreateAsync(
			user: User.Create(
				role: ae.Role,
				username: ae.Username,
				email: new(ae.Email),
				accountId: ae.Id
			),
			password: ae.Password
		).ConfigureAwait(false);
	}
}
