using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Carts;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;

public class CartPaymentCompletedApplicationEventHandler(IPurchasedCartReads reads, IUnitOfWork uow, IRequestSender sender, IEmailService email)
{
	public async Task Handle(CartPaymentCompletedApplicationEvent ae)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(ae.Id).ConfigureAwait(false)
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

		await email.SendRewardGrantedEmailAsync(to, $"{url}/carts").ConfigureAwait(false);
	}
}
