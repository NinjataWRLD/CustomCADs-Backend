using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow)
{
    public async Task<Guid> Handle(CreateUserCommand req, CancellationToken ct)
    {
        User user = new()
        {
            RoleName = req.Dto.RoleName,
            Username = req.Dto.Username,
            Email = req.Dto.Email,
            NameInfo = new() 
            {
                FirstName = req.Dto.FirstName, 
                LastName = req.Dto.LastName,
            },
        };
        await writes.AddAsync(user, ct).ConfigureAwait(false);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
        return user.Id;
    }
}
