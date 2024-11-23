using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Single;

public record DesignerGetOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string Status,
    ImageDto Image,
    Guid BuyerId,
    Guid? CadId,
    Guid? ShipmentId
);
