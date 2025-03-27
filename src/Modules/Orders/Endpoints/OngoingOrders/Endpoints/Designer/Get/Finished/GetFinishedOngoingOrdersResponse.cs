namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Finished;

public sealed record GetFinishedOngoingOrdersResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string BuyerName,
    bool Delivery
);