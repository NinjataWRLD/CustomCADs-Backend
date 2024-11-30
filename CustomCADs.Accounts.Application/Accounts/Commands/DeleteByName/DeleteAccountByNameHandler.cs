using CustomCADs.Accounts.Application.Accounts.Exceptions;
using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;

namespace CustomCADs.Accounts.Application.Accounts.Commands.DeleteByName;

public class DeleteAccountByNameHandler(IAccountReads reads, IWrites<Account> writes, IUnitOfWork uow, IEventRaiser raiser)
    : ICommandHandler<DeleteAccountByNameCommand>
{
    public async Task Handle(DeleteAccountByNameCommand req, CancellationToken ct)
    {
        Account user = await reads.SingleByUsernameAsync(req.Username, ct: ct).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ByUsername(req.Username);

        writes.Remove(user);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await raiser.RaiseIntegrationEventAsync(new AccountDeletedIntegrationEvent(
            req.Username
        )).ConfigureAwait(false);
    }
}
