namespace CustomCADs.Inventory.Endpoints.Products.Get.Stats;

public record ProductsStatsResponse(
    int UncheckedCount,
    int ValidatedCount,
    int ReportedCount,
    int BannedCount
);
