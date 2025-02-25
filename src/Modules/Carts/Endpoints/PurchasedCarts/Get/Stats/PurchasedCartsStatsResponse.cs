namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Stats;

public sealed record PurchasedCartsStatsResponse(
    int Total,
    Dictionary<Guid, int> Counts
);
