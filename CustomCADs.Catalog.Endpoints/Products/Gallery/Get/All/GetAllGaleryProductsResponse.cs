using CustomCADs.Catalog.Endpoints.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.All;

public sealed record GetAllGaleryProductsResponse(
    Guid Id,
    string Name,
    ImageDto Image
);
