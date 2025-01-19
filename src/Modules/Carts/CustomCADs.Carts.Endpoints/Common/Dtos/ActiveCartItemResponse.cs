namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record ActiveCartItemResponse(
    Guid Id,
    int Quantity,
    bool ForDelivery,
    double Weight,
    Guid ProductId,
    Guid CartId
);