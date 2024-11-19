using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Gallery.Endpoints.Carts.Get;

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