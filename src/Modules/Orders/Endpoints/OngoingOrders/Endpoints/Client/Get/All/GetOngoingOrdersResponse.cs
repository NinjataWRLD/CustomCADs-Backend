namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.All;

public sealed record GetOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string OrderStatus,
    bool Delivery
);