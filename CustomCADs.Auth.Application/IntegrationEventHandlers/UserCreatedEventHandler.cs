using CustomCADs.Auth.Domain.Common.Exceptions.Users;
using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Auth.Application.IntegrationEventHandlers;

public class UserCreatedEventHandler(IUserService service)
{
    public async Task Handle(UserCreatedIntegrationEvent ie)
    {
        AppUser user = new(ie.Username, ie.Email) { AccountId = ie.Id };
        IdentityResult result = await service.CreateAsync(user, ie.Password).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw UserValidationException.CouldNotCreateUser(ie.Username);
        }
        await service.AddToRoleAsync(user, ie.Role).ConfigureAwait(false);
    }
}
