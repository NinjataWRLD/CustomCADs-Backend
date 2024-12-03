using CustomCADs.Gallery.Domain.Carts.Enums;

namespace CustomCADs.Gallery.Endpoints.Carts.Post.Items;

public sealed record PostCartItemRequest(
    Guid CartId,
    DeliveryType DeliveryType,
    int Quantity,
    Guid ProductId
);
