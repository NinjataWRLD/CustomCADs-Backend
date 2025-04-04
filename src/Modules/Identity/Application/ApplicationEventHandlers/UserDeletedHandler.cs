using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class UserDeletedHandler(IUserManager manager)
{
    public async Task Handle(AccountDeletedApplicationEvent ae)
    {
        AppUser user = await manager.GetByUsernameAsync(ae.Username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(ae.Username);

        await manager.DeleteAsync(user).ConfigureAwait(false);
    }
}
