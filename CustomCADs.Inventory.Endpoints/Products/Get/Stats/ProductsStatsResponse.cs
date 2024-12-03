namespace CustomCADs.Inventory.Endpoints.Products.Get.Stats;

public sealed record ProductsStatsResponse(
    int UncheckedCount,
    int ValidatedCount,
    int ReportedCount,
    int BannedCount
);
