using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.Users.EventHandlers.Application;

public class UserCreatedHandler(IUserManager manager)
{
    public async Task Handle(AccountCreatedApplicationEvent ae)
    {
        await manager.AddAsync(
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
