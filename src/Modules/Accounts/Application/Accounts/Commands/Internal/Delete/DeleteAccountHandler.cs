using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;

public sealed class DeleteAccountHandler(IAccountReads reads, IAccountWrites writes, IUnitOfWork uow, IEventRaiser raiser)
	: ICommandHandler<DeleteAccountCommand>
{
	public async Task Handle(DeleteAccountCommand req, CancellationToken ct)
	{
		Account account = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(req.Id);

		writes.Remove(account);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(new AccountDeletedApplicationEvent(
			account.Username
		)).ConfigureAwait(false);
	}
}
