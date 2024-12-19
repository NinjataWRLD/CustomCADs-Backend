namespace CustomCADs.Carts.Endpoints.Carts.Post.Items;

public sealed record PostCartItemRequest(
    Guid CartId,
    int Quantity,
    double Weight,
    Guid ProductId,
    bool Delivery
);
