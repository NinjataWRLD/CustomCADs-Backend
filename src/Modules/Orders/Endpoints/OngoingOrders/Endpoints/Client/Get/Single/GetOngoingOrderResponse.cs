namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Single;

public sealed record GetOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string OrderStatus,
    bool Delivery,
    string? DesignerName
);
