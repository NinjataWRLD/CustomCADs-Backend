using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetSortings;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Sortings;

public sealed class GetPurchasedCartSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<string[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<PurchasedCartsGroup>();
		Description(d => d
			.WithSummary("Sortings")
			.WithDescription("See all Purchased Cart Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		string[] result = await sender.SendQueryAsync(
			new GetPurchasedCartSortingsQuery(),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
