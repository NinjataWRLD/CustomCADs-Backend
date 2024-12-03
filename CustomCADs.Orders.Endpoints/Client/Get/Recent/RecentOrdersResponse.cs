namespace CustomCADs.Orders.Endpoints.Client.Get.Recent;

public sealed record RecentOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string? DesignerName
);
