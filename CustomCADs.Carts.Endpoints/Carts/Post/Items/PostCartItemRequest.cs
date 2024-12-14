namespace CustomCADs.Carts.Endpoints.Carts.Post.Items;

public sealed record PostCartItemRequest(
    Guid CartId,
    int Quantity,
    Guid ProductId,
    bool Delivery = false
);
