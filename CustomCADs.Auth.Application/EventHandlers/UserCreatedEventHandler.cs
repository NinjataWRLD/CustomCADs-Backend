using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Auth.Application.EventHandlers;

public class UserCreatedEventHandler(IUserService service)
{
    public async Task Handle(UserCreatedIntegrationEvent ie)
    {
        AppUser user = new(ie.Username, ie.Email) { AccountId = ie.Id };
        IdentityResult result = await service.CreateAsync(user, ie.Password).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw new UserException($"Couldn't create the user: {ie.Username}.");
        }
        await service.AddToRoleAsync(user, ie.Role).ConfigureAwait(false);
    }
}
