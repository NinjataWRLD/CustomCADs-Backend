using CustomCADs.Auth.Domain.Common.Exceptions.Users;
using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Auth.Application.IntegrationEventHandlers;

public class UserDeletedEventHandler(IUserService service)
{
    public async Task Handle(UserDeletedIntegrationEvent ie)
    {
        AppUser user = await service.FindByNameAsync(ie.Username).ConfigureAwait(false)
            ?? throw
            UserNotFoundException.ByUsername(ie.Username);

        await service.DeleteAsync(user).ConfigureAwait(false);
    }
}
