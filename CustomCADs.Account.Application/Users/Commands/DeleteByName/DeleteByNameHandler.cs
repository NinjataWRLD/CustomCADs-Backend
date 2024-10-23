using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Users.Commands.DeleteByName;

public class DeleteUserByNameHandler(
    IUserReads reads,
    IWrites<User> writes,
    IUnitOfWork uow)
{
    public async Task Handle(DeleteUserByNameCommand req, CancellationToken ct)
    {
        User user = await reads.SingleByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException($"The User with name: {req.Username} does not exist.");

        writes.Remove(user);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
