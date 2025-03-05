namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record PurchasedCartItemResponse(
    int Quantity,
    bool ForDelivery,
    decimal Price,
    decimal Cost,
    Guid ProductId,
    Guid CartId,
    Guid? CustomizationId
);