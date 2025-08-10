using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Identity;

namespace CustomCADs.Accounts.Application.Accounts.Events.Application;

public class UserDeletedHandler(IAccountReads reads, IAccountWrites writes, IUnitOfWork uow)
{
	public async Task Handle(UserDeletedApplicationEvent ae)
	{
		Account account = await reads.SingleByIdAsync(ae.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(ae.Id);

		writes.Remove(account);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
