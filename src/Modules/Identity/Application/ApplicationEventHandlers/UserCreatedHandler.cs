using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class UserCreatedHandler(IUserManager manager)
{
    public async Task Handle(AccountCreatedApplicationEvent ae)
    {
        AppUser user = new(ae.Username, ae.Email, ae.Id);
        await manager.AddAsync(ae.Role, user, ae.Password).ConfigureAwait(false);
    }
}
