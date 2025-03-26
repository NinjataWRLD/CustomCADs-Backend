namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Recent;

public sealed record RecentOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
