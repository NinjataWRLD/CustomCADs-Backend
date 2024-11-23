using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.Client.Get.Single;

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
