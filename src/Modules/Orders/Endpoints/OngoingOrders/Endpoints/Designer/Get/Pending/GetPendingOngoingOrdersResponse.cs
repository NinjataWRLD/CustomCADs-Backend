namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Pending;

public sealed record GetPendingOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string BuyerName,
    bool Delivery
);