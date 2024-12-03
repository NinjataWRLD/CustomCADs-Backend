namespace CustomCADs.Gallery.Endpoints.Carts.Get.PresignedCadUrl;

public sealed record GetCartItemGetPresignedCadUrlRequest(
    Guid Id, 
    Guid ItemId
);
