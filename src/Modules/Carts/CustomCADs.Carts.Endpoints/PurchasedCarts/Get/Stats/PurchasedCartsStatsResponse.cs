namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Stats;

public sealed record PurchasedCartsStatsResponse(
    int TotalCount,
    Dictionary<Guid, int> Counts
);
