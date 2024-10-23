using CustomCADs.Account.Application.Users.Common.Exceptions;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Application.Users.Commands.DeleteById;

public class DeleteUserByIdHandler(
    IUserReads reads,
    IWrites<User> writes,
    IUnitOfWork uow)
{
    public async Task Handle(DeleteUserByIdCommand req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException($"The User with id: {req.Id} does not exist.");

        writes.Remove(user);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
