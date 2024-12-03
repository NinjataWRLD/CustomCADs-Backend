namespace CustomCADs.Carts.Endpoints.Carts.Get.PresignedCadUrl;

public sealed record GetCartItemGetPresignedCadUrlRequest(
    Guid Id, 
    Guid ItemId
);
