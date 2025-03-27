namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Cart;

public sealed record PostActiveCartResponse(
    Guid Id,
    string BuyerName
);