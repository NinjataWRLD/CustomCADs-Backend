namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Pending;

public sealed record GetPendingOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string BuyerName,
    bool Delivery
);