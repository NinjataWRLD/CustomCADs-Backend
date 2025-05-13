using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed record GetPurchasedCartItemGetPresignedCadUrlResponse(
    string PresignedUrl,
    string ContentType,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates
);
