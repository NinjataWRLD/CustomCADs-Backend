namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record PurchasedCartItemResponse(
    Guid Id,
    int Quantity,
    bool Delivery,
    decimal Price,
    Guid ProductId,
    Guid CartId,
    Guid CadId,
    decimal Cost
);