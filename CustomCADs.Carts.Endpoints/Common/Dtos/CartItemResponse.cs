namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record CartItemResponse(
    Guid Id,
    int Quantity,
    bool Delivery,
    decimal Price,
    string PurchaseDate,
    Guid ProductId,
    Guid CartId,
    Guid? CadId,
    Guid? ShipmentId,
    decimal Cost
);