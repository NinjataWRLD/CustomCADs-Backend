using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class UserDeletedHandler(IUserService service)
{
    public async Task Handle(AccountDeletedApplicationEvent ae)
    {
        AppUser user = await service.FindByNameAsync(ae.Username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(ae.Username);

        await service.DeleteAsync(user).ConfigureAwait(false);
    }
}
