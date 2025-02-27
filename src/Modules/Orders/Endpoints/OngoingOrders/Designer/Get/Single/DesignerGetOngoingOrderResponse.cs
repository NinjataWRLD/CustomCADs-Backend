namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Single;

public sealed record DesignerGetOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string Status,
    bool Delivery,
    string BuyerName
);
