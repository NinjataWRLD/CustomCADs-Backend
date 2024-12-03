namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.Recent;

public sealed record RecentOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
