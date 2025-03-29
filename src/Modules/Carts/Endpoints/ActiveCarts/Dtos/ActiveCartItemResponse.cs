namespace CustomCADs.Carts.Endpoints.ActiveCarts.Dtos;

public sealed record ActiveCartItemResponse(
    int Quantity,
    bool ForDelivery,
    string BuyerName,
    Guid ProductId,
    Guid? CustomizationId
);