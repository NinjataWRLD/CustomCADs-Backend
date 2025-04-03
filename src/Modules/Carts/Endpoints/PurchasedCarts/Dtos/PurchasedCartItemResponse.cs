namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Dtos;

public sealed record PurchasedCartItemResponse(
    int Quantity,
    bool ForDelivery,
    decimal Price,
    decimal Cost,
    string AddedAt,
    Guid ProductId,
    Guid CartId,
    Guid? CustomizationId
);