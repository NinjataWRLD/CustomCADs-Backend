namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer.Get.All;

public sealed record DesignerGetCompletedOrdersResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string PurchasedAt,
    string BuyerName,
    bool Delivery
);