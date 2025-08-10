using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.PaymentStatuses;

public sealed class GetPurchasedCartPaymentStatusesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<string[]>
{
	public override void Configure()
	{
		Get("payment-statuses");
		Group<PurchasedCartsGroup>();
		Description(d => d
			.WithSummary("Payment Statuses")
			.WithDescription("See all Purchased Cart Payment Status types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		string[] result = await sender.SendQueryAsync(
			new GetPurchasedCartPaymentStatusesQuery(),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
