namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record CartItemResponse(
    Guid Id,
    int Quantity,
    bool Delivery,
    double Weight,
    decimal Price,
    string PurchaseDate,
    Guid ProductId,
    Guid CartId,
    Guid? CadId,
    decimal Cost
);