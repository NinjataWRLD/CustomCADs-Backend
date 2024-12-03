using CustomCADs.Carts.Domain.Carts.Enums;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Items;

public sealed record PostCartItemRequest(
    Guid CartId,
    DeliveryType DeliveryType,
    int Quantity,
    Guid ProductId
);
