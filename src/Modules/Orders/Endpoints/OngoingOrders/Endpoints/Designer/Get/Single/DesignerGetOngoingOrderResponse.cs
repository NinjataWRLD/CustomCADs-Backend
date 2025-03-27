namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Single;

public sealed record DesignerGetOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderedAt,
    string Status,
    bool Delivery,
    string BuyerName
);
