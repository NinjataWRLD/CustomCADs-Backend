using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

public class GetPurchasedCartPaymentStatusesHandler
	: IQueryHandler<GetPurchasedCartPaymentStatusesQuery, string[]>
{
	public Task<string[]> Handle(GetPurchasedCartPaymentStatusesQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetNames<PaymentStatus>()
		);
}
