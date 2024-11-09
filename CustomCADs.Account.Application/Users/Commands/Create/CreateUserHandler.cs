using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow, IEventRaiser raiser)
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

        await raiser.RaiseAsync(new UserCreatedIntegrationEvent(
            Id: user.Id,
            Role: user.RoleName,
            Username: user.Username,
            Email: user.Email,
            Password: req.Password
        )).ConfigureAwait(false);

        return user.Id;
    }
}
