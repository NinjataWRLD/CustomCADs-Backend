namespace CustomCADs.Gallery.Endpoints.Carts.Get.Stats;

public sealed record CartsStatsResponse(
    int TotalCount,
    Dictionary<Guid, int> Counts
);
