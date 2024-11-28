using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.UseCases.Users.Commands;

namespace CustomCADs.Account.Application.Users.SharedCommandHandlers;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow)
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

        return user.Id;
    }
}
