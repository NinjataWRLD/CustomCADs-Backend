using CustomCADs.Catalog.Endpoints.Helpers.Dtos;

namespace CustomCADs.Catalog.Endpoints.Gallery.Get.All;

public sealed record GetAllGaleryProductsResponse(
    Guid Id,
    string Name,
    ImageDto Image
);
