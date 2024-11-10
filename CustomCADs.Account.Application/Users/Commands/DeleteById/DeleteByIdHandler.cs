﻿using CustomCADs.Account.Domain.Shared;
using CustomCADs.Account.Domain.Users.Reads;

namespace CustomCADs.Account.Application.Users.Commands.DeleteById;

public class DeleteUserByIdHandler(IUserReads reads, IWrites<User> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteUserByIdCommand>
{
    public async Task Handle(DeleteUserByIdCommand req, CancellationToken ct)
    {
        User user = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(req.Id);

        writes.Remove(user);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
