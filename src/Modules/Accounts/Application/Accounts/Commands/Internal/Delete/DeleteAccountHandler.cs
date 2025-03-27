using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;

public sealed class DeleteAccountHandler(IAccountReads reads, IWrites<Account> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteAccountCommand>
{
    public async Task Handle(DeleteAccountCommand req, CancellationToken ct)
    {
        Account account = await reads.SingleByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Account>.ByProp(nameof(req.Username), req.Username);

        writes.Remove(account);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseApplicationEventAsync(new AccountDeletedApplicationEvent(
            req.Username
        )).ConfigureAwait(false);
    }
}
