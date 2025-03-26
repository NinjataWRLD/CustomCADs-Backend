namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Begun;

public sealed record GetBegunOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string BuyerName,
    bool Delivery
);