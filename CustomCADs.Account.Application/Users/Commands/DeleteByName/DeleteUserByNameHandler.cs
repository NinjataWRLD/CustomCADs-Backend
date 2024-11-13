using CustomCADs.Account.Domain.Common;
using CustomCADs.Account.Domain.Users.Entities;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Users;

namespace CustomCADs.Account.Application.Users.Commands.DeleteByName;

public class DeleteUserByNameHandler(IUserReads reads, IWrites<User> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteUserByNameCommand>
{
    public async Task Handle(DeleteUserByNameCommand req, CancellationToken ct)
    {
        User user = await reads.SingleByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(req.Username);

        writes.Remove(user);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseAsync(new UserDeletedIntegrationEvent(
            req.Username
        )).ConfigureAwait(false);
    }
}
