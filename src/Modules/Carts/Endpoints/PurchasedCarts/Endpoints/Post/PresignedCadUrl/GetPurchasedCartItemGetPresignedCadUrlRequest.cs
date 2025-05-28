namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed record GetPurchasedCartItemGetPresignedCadUrlRequest(
	Guid Id,
	Guid ProductId
);
