using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.Single;

public record CartItemDto(
    Guid Id,
    int Quantity,
    string DeliveryType,
    MoneyDto Price,
    string PurchaseDate,
    Guid ProductId,
    Guid CartId,
    Guid? CadId,
    Guid? ShipmentId,
    MoneyDto Cost
);