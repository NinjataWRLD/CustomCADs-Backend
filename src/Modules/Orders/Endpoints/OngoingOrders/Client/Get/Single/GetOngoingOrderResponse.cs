namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Single;

public sealed record GetOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string OrderStatus,
    bool Delivery,
    string? DesignerName
);
