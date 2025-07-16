using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Identity;

namespace CustomCADs.Accounts.Application.Accounts.Events.Application;

public class UserEditedHandler(IAccountReads reads, IUnitOfWork uow)
{
	public async Task Handle(UserEditedApplicationEvent ae)
	{
		Account account = await reads.SingleByIdAsync(ae.Id).ConfigureAwait(false)
			?? throw CustomNotFoundException<Account>.ById(ae.Id);

		if (ae.Username is not null)
		{
			account.SetUsername(ae.Username);
		}
		if (ae.TrackViewedProducts is not null)
		{
			account.SetTrackViewedProducts(ae.TrackViewedProducts.Value);
		}
		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
