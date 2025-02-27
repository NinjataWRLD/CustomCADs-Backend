namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record ActiveCartItemResponse(
    int Quantity,
    bool ForDelivery,
    double Weight,
    Guid ProductId,
    Guid CartId
);