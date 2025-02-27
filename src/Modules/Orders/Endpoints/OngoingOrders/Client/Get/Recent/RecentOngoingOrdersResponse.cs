namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Recent;

public sealed record RecentOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
