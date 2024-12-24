namespace CustomCADs.Carts.Endpoints.Carts.Post.Item;

public sealed record PostCartItemRequest(
    Guid CartId,
    double Weight,
    Guid ProductId
);
