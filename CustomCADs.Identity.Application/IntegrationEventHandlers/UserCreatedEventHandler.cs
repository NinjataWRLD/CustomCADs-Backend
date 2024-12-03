using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;

namespace CustomCADs.Identity.Application.IntegrationEventHandlers;

public class UserCreatedEventHandler(IUserService service)
{
    public async Task Handle(AccountCreatedIntegrationEvent ie)
    {
        AppUser user = new(ie.Username, ie.Email, ie.Id);
        IdentityResult result = await service.CreateAsync(user, ie.Password).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw UserCreationException.ByUsername(ie.Username);
        }
        await service.AddToRoleAsync(user, ie.Role).ConfigureAwait(false);
    }
}
