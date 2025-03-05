namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record ActiveCartItemResponse(
    int Quantity,
    bool ForDelivery,
    Guid ProductId,
    Guid CartId,
    Guid? CustomizationId
);