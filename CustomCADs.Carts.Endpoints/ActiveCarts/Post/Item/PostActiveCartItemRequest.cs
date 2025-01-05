namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.Item;

public sealed record PostActiveCartItemRequest(
    Guid CartId,
    double Weight,
    Guid ProductId
);
