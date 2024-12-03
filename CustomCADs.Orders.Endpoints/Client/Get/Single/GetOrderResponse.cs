namespace CustomCADs.Orders.Endpoints.Client.Get.Single;

public sealed record GetOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus,
    Guid? DesignerId,
    Guid? CadId,
    Guid? ShipmentId
);
