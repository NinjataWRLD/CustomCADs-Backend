namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Item;

public sealed record PostActiveCartItemRequest(
    Guid ProductId,
    Guid? CustomizationId,
    bool ForDelivery
);
