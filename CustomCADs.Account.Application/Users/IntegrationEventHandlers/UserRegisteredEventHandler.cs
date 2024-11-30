using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.IntegrationEvents.Auth;

namespace CustomCADs.Account.Application.Users.IntegrationEventHandlers;

public class UserRegisteredEventHandler(IWrites<User> writes, IUnitOfWork uow)
{
    public async Task Handle(UserRegisteredIntegrationEvent ie)
    {
        var user = User.Create(
            ie.Role,
            ie.Username,
            ie.Email,
            ie.TimeZone,
            ie.FirstName,
            ie.LastName
        );

        await writes.AddAsync(user).ConfigureAwait(false);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
