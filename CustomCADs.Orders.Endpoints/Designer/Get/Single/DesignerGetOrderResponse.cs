namespace CustomCADs.Orders.Endpoints.Designer.Get.Single;

public sealed record DesignerGetOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string Status,
    Guid BuyerId,
    Guid? CadId,
    Guid? ShipmentId
);
