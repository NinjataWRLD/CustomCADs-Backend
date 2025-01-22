namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.Cart;

public sealed record PostActiveCartResponse(
    Guid Id,
    string BuyerName
);