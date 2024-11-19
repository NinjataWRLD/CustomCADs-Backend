using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Get;

public record GetCustomOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus,
    ImageDto Image,
    Guid BuyerId,
    Guid? DesignerId,
    Guid? CadId,
    Guid? ShipmentId
);
