namespace CustomCADs.Orders.Endpoints.Client.Get.Stats;

public record OrdersStatsResponse(
    int PendingCount,
    int AcceptedCount,
    int BegunCount,
    int FinishedCount,
    int ReportedCount,
    int RemovedCount
);
