namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record ActiveCartItemResponse(
    Guid Id,
    int Quantity,
    bool Delivery,
    double Weight,
    Guid ProductId,
    Guid CartId
);