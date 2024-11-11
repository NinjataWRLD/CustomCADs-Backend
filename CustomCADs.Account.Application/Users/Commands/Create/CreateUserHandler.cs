using CustomCADs.Account.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<UserId> Handle(CreateUserCommand req, CancellationToken ct)
    {
        User user = User.Create(req.Role, req.Username, req.Email, req.FirstName, req.LastName);
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
