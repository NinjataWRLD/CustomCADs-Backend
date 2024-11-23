namespace CustomCADs.Orders.Endpoints.Client.Get.Recent;

public record RecentOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
