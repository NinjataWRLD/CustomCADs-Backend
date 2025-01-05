namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.ItemWithDelivery;

public sealed record PostActiveCartItemWithDeliveryRequest(
    double Weight,
    Guid ProductId
);
