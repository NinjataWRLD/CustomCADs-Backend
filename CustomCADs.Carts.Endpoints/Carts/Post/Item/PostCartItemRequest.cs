namespace CustomCADs.Carts.Endpoints.Carts.Post.Item;

public sealed record PostCartItemRequest(
    Guid CartId,
    int Quantity,
    double Weight,
    Guid ProductId
);
