namespace CustomCADs.Carts.Endpoints.Carts.Post.ItemWithDelivery;

public sealed record PostCartItemWithDeliveryRequest(
    Guid CartId,
    int Quantity,
    double Weight,
    Guid ProductId
);
