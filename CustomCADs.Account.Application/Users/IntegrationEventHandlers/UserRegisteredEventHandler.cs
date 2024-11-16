using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Users.Entities;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Users;
using CustomCADs.Shared.IntegrationEvents.Auth;

namespace CustomCADs.Account.Application.Users.IntegrationEventHandlers;

public class UserRegisteredEventHandler(IWrites<User> writes, IUnitOfWork uow, IEventRaiser raiser)
{
    public async Task Handle(UserRegisteredIntegrationEvent ie)
    {
        var user = User.Create(
            ie.Role, 
            ie.Username, 
            ie.Email, 
            ie.FirstName, 
            ie.LastName
        );
        
        await writes.AddAsync(user).ConfigureAwait(false);
        await uow.SaveChangesAsync().ConfigureAwait(false);

        await raiser.RaiseAsync(new UserAccountCreatedIntegrationEvent(
            Id: user.Id,
            Role: user.RoleName,
            Username: user.Username,
            Email: user.Email
        )).ConfigureAwait(false);
    }
}
