namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.All;

public sealed record GetOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string OrderStatus,
    bool Delivery
);