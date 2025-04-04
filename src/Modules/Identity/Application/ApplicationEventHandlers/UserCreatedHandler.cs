using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class UserCreatedHandler(IUserRepository repository)
{
    public async Task Handle(AccountCreatedApplicationEvent ae)
    {
        AppUser user = new(ae.Username, ae.Email, ae.Id);
        await repository.AddAsync(ae.Role, user, ae.Password).ConfigureAwait(false);
        await repository.SaveChangesAsync().ConfigureAwait(false);
    }
}
