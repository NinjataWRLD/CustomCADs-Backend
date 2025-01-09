namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Stats;

public sealed record GetOngoingOrdersStatsResponse(
    int PendingCount,
    int AcceptedCount,
    int BegunCount,
    int FinishedCount,
    int ReportedCount,
    int RemovedCount
);
