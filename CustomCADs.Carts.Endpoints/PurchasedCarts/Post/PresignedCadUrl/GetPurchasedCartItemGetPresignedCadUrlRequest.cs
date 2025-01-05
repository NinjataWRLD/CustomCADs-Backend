namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Post.PresignedCadUrl;

public sealed record GetPurchasedCartItemGetPresignedCadUrlRequest(
    Guid Id,
    Guid ItemId
);
