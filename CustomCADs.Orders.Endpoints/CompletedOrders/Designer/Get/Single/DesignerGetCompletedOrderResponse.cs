namespace CustomCADs.Orders.Endpoints.CompletedOrders.Designer.Get.Single;

public sealed record DesignerGetCompletedOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string PurchaseDate,
    bool Delivery,
    Guid BuyerId,
    Guid CadId,
    Guid? ShipmentId
);
