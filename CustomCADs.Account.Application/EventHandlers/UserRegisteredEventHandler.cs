using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Account.Application.EventHandlers;

public class UserRegisteredEventHandler(IWrites<User> writes, IUnitOfWork uow)
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
    }
}
