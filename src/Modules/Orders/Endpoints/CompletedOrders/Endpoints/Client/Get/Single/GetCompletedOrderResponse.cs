namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.Single;

public sealed record GetCompletedOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderedAt,
    string PurchasedAt,
    bool Delivery,
    string DesignerName,
    Guid? ShipmentId
);
