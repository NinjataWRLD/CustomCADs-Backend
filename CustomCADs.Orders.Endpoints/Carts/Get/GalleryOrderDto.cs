using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Orders.Endpoints.Carts.Get;

public record GalleryOrderDto(
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