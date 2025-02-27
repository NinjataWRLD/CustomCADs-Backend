namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Accepted;

public sealed record GetAcceptedOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string BuyerName,
    bool Delivery
);