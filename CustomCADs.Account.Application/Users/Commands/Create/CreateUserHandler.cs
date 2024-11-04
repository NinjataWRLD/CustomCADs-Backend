using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Core.Events;
using Wolverine;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow, IMessageBus bus)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand req, CancellationToken ct)
    {
        User user = new()
        {
            RoleName = req.Role,
            Username = req.Username,
            Email = req.Email,
            NameInfo = new() 
            {
                FirstName = req.FirstName, 
                LastName = req.LastName,
            },
            Role = null!,
        };
        await writes.AddAsync(user, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        UserCreatedEvent ucEvent = new(req.Role, req.Username, req.Email, req.Password);
        await bus.PublishAsync(ucEvent).ConfigureAwait(false);

        return user.Id;
    }
}
