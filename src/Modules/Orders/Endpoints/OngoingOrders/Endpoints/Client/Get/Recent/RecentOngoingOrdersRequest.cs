namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Recent;

public sealed record RecentOngoingOrdersRequest(
    int Limit = 5
);
