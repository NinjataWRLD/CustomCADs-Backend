using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

public record GetPurchasedCartItemCadPresignedUrlGetDto(
	string PresignedUrl,
	string ContentType,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates
);
