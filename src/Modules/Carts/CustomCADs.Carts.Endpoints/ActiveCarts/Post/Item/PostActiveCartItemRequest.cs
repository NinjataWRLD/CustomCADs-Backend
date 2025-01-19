namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.Item;

public sealed record PostActiveCartItemRequest(
    Guid ProductId,
    double Weight,
    bool ForDelivery
);
