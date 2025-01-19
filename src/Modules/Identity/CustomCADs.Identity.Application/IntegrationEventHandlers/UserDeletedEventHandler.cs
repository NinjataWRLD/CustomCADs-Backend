using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.IntegrationEventHandlers;

public class UserDeletedEventHandler(IUserService service)
{
    public async Task Handle(AccountDeletedIntegrationEvent ie)
    {
        AppUser user = await service.FindByNameAsync(ie.Username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(ie.Username);

        await service.DeleteAsync(user).ConfigureAwait(false);
    }
}
