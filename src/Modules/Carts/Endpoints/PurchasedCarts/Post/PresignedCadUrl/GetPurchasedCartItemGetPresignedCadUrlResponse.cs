namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Post.PresignedCadUrl;

public sealed record GetPurchasedCartItemGetPresignedCadUrlResponse(
    string PresignedUrl,
    string ContentType
);
