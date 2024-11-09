using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Auth.Application.EventHandlers;

public class UserDeletedEventHandler(IUserService service)
{
    public async Task Handle(UserDeletedIntegrationEvent ie)
    {
        AppUser user = await service.FindByNameAsync(ie.Username).ConfigureAwait(false)
            ?? throw new UserNotFoundException(username: ie.Username);

        await service.DeleteAsync(user).ConfigureAwait(false);
    }
}
