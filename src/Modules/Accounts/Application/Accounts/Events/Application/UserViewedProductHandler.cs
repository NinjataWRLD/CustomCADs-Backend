using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.ApplicationEvents.Catalog;

namespace CustomCADs.Accounts.Application.Accounts.Events.Application;

public class UserViewedProductHandler(IAccountWrites writes, IUnitOfWork uow)
{
	public async Task Handle(UserViewedProductApplicationEvent ae)
	{
		await writes.ViewProductAsync(ae.AccountId, ae.Id).ConfigureAwait(false);
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
