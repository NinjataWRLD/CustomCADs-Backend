using CustomCADs.Accounts.Application.Accounts.Exceptions;
using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.Accounts.Application.Accounts.Commands.DeleteById;

public class DeleteAccountByIdHandler(IAccountReads reads, IWrites<Account> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteAccountByIdCommand>
{
    public async Task Handle(DeleteAccountByIdCommand req, CancellationToken ct)
    {
        Account account = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ById(req.Id);

        writes.Remove(account);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
