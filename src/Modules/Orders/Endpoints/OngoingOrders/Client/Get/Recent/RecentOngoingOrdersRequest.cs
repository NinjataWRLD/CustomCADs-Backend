namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Recent;

public sealed record RecentOngoingOrdersRequest(
    int Limit = 5
);
