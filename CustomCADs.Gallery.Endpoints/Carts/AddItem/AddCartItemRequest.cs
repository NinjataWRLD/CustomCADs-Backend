using CustomCADs.Gallery.Domain.Carts.Enums;

namespace CustomCADs.Gallery.Endpoints.Carts.AddItem;

public record AddCartItemRequest(
    Guid CartId,
    DeliveryType DeliveryType,
    int Quantity,
    Guid ProductId
);
