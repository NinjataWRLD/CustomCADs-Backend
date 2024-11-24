namespace CustomCADs.Orders.Endpoints.Designer.Get.Single;

public record DesignerGetOrderResponse(
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
