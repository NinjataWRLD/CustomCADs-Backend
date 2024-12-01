namespace CustomCADs.Gallery.Endpoints.Carts.Get.Stats;

public record CartsStatsResponse(
    int TotalCount,
    Dictionary<Guid, int> Counts
);
