namespace CustomCADs.Orders.Endpoints.Client.Get.Stats;

public sealed record OrdersStatsResponse(
    int PendingCount,
    int AcceptedCount,
    int BegunCount,
    int FinishedCount,
    int CompletedCount,
    int ReportedCount,
    int RemovedCount
);
