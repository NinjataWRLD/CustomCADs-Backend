namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.Single;

public sealed record GetCompletedOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string PurchaseDate,
    bool Delivery,
    string DesignerName,
    Guid? ShipmentId
);
