namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer.Get.All;

public sealed record DesignerGetCompletedOrdersResponse(
    Guid Id,
    string Name,
    string OrderDate,
    string PurchaseDate,
    string BuyerName,
    bool Delivery
);