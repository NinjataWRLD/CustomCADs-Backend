namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record CartItemDto(
    Guid Id,
    int Quantity,
    string DeliveryType,
    decimal Price,
    string PurchaseDate,
    Guid ProductId,
    Guid CartId,
    Guid? CadId,
    Guid? ShipmentId,
    decimal Cost
);