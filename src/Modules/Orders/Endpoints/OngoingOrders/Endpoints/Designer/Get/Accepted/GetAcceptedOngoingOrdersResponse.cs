namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Accepted;

public sealed record GetAcceptedOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string BuyerName,
    bool Delivery
);