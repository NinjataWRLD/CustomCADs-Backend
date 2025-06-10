using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Email;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Carts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Identity.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Events.Application;

public class CartPaymentCompletedApplicationEventHandler(IPurchasedCartReads reads, IUnitOfWork uow, IRequestSender sender, IEmailService email)
{
	public async Task Handle(CartPaymentCompletedApplicationEvent ae)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(ae.Id, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(ae.Id);

		cart.FinishPayment(success: true);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		await uow.BulkDeleteItemsByBuyerIdAsync(ae.BuyerId).ConfigureAwait(false);

		string to = await sender.SendQueryAsync(
			new GetUserEmailByIdQuery(ae.BuyerId)
		).ConfigureAwait(false);

		string url = await sender.SendQueryAsync(
			new GetClientUrlQuery()
		).ConfigureAwait(false);

		await email.SendRewardGrantedEmailAsync(to, url).ConfigureAwait(false);
	}
}
