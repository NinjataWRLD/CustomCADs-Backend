namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Reported;

public sealed record GetReportedOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string BuyerName,
    bool Delivery
);