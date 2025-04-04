using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.ApplicationEventHandlers;

public class UserDeletedHandler(IUserRepository repository)
{
    public async Task Handle(AccountDeletedApplicationEvent ae)
    {
        AppUser user = await repository.GetByUsernameAsync(ae.Username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(ae.Username);

        repository.Remove(user);
        await repository.SaveChangesAsync().ConfigureAwait(false);
    }
}
