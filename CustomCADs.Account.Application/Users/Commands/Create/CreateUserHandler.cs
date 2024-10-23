using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Shared.Domain;
using Mapster;

namespace CustomCADs.Account.Application.Users.Commands.Create;

public class CreateUserHandler(IWrites<User> writes, IUnitOfWork uow)
{
    public async Task<Guid> Handle(CreateUserCommand req, CancellationToken ct)
    {
        User user = req.Dto.Adapt<User>();

        await writes.AddAsync(user, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return user.Id;
    }
}
