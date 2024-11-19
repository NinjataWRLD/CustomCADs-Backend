namespace CustomCADs.Orders.Endpoints.Orders.Recent;

public record RecentOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
