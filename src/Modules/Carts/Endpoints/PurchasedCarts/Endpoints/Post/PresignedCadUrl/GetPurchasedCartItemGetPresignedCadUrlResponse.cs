using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed record GetPurchasedCartItemGetPresignedCadUrlResponse(
	string PresignedUrl,
	string ContentType,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates
);
