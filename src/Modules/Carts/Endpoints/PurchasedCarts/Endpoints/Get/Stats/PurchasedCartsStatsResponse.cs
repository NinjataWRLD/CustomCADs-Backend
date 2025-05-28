namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Stats;

public sealed record PurchasedCartsStatsResponse(
	int Total,
	Dictionary<Guid, int> Counts
);
