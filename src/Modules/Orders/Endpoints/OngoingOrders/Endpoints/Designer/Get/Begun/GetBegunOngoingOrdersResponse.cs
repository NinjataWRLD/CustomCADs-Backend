namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Begun;

public sealed record GetBegunOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string BuyerName,
    bool Delivery
);