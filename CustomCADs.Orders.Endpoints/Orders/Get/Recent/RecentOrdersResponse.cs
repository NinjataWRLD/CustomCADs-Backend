namespace CustomCADs.Orders.Endpoints.Orders.Get.Recent;

public record RecentOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
