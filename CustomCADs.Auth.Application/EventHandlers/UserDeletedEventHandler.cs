using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Exceptions;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Core.Events.Users;

namespace CustomCADs.Auth.Application.EventHandlers;

public class UserDeletedEventHandler(IUserService service)
{
    public async Task Handle(UserDeletedEvent udEvent)
    {
        AppUser user = await service.FindByNameAsync(udEvent.Username).ConfigureAwait(false)
            ?? throw new UserNotFoundException(udEvent.Username, new { });

        await service.DeleteAsync(user).ConfigureAwait(false);
    }
}
