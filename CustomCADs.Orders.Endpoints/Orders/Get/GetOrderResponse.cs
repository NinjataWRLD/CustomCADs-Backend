using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.Orders.Get;

public record GetOrderResponse(
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
