using CustomCADs.Account.Application.Common.Contracts;
using CustomCADs.Account.Application.Common.Exceptions;
using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users;
using CustomCADs.Account.Domain.Users.Reads;
using CustomCADs.Shared.Core.Events.Users;
using Wolverine;

namespace CustomCADs.Account.Application.Users.Commands.DeleteByName;

public class DeleteUserByNameHandler(IUserReads reads, IWrites<User> writes, IUnitOfWork uow, IMessageBus bus)
    : ICommandHandler<DeleteUserByNameCommand>
{
    public async Task Handle(DeleteUserByNameCommand req, CancellationToken ct)
    {
        User user = await reads.SingleByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false)
            ?? throw new UserNotFoundException(req.Username, new { });

        writes.Remove(user);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        UserDeletedEvent udEvent = new(req.Username);
        await bus.PublishAsync(udEvent).ConfigureAwait(false);
    }
}
