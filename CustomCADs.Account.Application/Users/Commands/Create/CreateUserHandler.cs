using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<UserId> Handle(CreateUserCommand req, CancellationToken ct)
    {
        var user = User.Create(
            req.Role,
            req.Username,
            req.Email,
            req.FirstName,
            req.LastName
        );

        await writes.AddAsync(user, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new UserCreatedIntegrationEvent(
            Id: user.Id,
            Role: user.RoleName,
            Username: user.Username,
            Email: user.Email,
            Password: req.Password
        )).ConfigureAwait(false);

        return user.Id;
    }
}
