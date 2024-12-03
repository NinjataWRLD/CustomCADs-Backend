namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.Single;

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
