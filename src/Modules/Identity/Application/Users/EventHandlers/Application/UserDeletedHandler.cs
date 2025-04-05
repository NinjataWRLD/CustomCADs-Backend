using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.Users.EventHandlers.Application;

public class UserDeletedHandler(IUserManager manager)
{
    public async Task Handle(AccountDeletedApplicationEvent ae)
    {
        await manager.DeleteAsync(ae.Username).ConfigureAwait(false);
    }
}
