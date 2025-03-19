using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Delete;

public sealed class DeleteAccountHandler(IAccountReads reads, IWrites<Account> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteAccountCommand>
{
    public async Task Handle(DeleteAccountCommand req, CancellationToken ct)
    {
        Account account = await reads.SingleByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ByUsername(req.Username);

        writes.Remove(account);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new AccountDeletedIntegrationEvent(
            req.Username
        )).ConfigureAwait(false);
    }
}
