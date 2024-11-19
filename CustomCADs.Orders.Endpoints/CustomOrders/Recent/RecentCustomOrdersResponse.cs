namespace CustomCADs.Orders.Endpoints.CustomOrders.Recent;

public record RecentCustomOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
