using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class UserCreatedHandler(IUserService service)
{
    public async Task Handle(AccountCreatedApplicationEvent ae)
    {
        AppUser user = new(ae.Username, ae.Email, ae.Id);
        await service.CreateAsync(user, ae.Password).ConfigureAwait(false);
        await service.AddToRoleAsync(user, ae.Role).ConfigureAwait(false);
    }
}
