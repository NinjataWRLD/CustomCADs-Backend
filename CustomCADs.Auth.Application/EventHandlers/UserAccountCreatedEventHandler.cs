using CustomCADs.Shared.IntegrationEvents.Account;

namespace CustomCADs.Auth.Application.EventHandlers;

public class UserAccountCreatedEventHandler(IUserService service)
{
    public async Task Handle(UserAccountCreatedEvent uacEvent)
    {
        await service.UpdateAccountIdAsync(uacEvent.Username, uacEvent.Id);
    }
}
