using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account;
using CustomCADs.Shared.IntegrationEvents.Auth;

namespace CustomCADs.Account.Application.Users.IntegrationEventHandlers;

public class UserRegisteredEventHandler(IWrites<User> writes, IUnitOfWork uow, IEventRaiser raiser)
{
    public async Task Handle(UserRegisteredEvent urEvent)
    {
        User user = new()
        {
            RoleName = urEvent.Role,
            Username = urEvent.Username,
            Email = urEvent.Email,
            NameInfo = new()
            {
                FirstName = urEvent.FirstName,
                LastName = urEvent.LastName,
            },
            Role = null!,
        };
        await writes.AddAsync(user).ConfigureAwait(false);
        await uow.SaveChangesAsync().ConfigureAwait(false);

        UserAccountCreatedEvent uacEvent = new(user.Id, user.RoleName, user.Username, user.Email);
        await raiser.RaiseAsync(uacEvent).ConfigureAwait(false);
    }
}
