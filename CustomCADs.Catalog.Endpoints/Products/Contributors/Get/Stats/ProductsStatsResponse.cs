namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.Stats;

public sealed record ProductsStatsResponse(
    int UncheckedCount,
    int ValidatedCount,
    int ReportedCount,
    int BannedCount
);
