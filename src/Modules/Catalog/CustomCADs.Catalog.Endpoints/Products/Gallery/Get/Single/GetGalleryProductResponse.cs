using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.Single;

public sealed record GetGalleryProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadDate,
    string CreatorName,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CountsDto Counts,
    CategoryResponse Category
);
