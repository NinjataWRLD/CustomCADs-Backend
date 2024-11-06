using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Exceptions;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Core.Events;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Application.EventHandlers;

public class UserCreatedEventHandler(IUserService service)
{
    public async Task Handle(UserCreatedEvent ucEvent)
    {
        AppUser user = new(ucEvent.Username, ucEvent.Email) { AccountId = ucEvent.Id };
        IdentityResult result = await service.CreateAsync(user, ucEvent.Password).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw new UserException($"Couldn't create the user: {ucEvent.Username}.");
        }
        await service.AddToRoleAsync(user, ucEvent.Role).ConfigureAwait(false);
    }
}
