namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Dtos;

public sealed record PurchasedCartItemResponse(
    int Quantity,
    bool ForDelivery,
    decimal Price,
    decimal Cost,
    Guid ProductId,
    Guid CartId,
    Guid? CustomizationId
);