using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow)
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
        };
        await writes.AddAsync(user, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return user.Id;
    }
}
