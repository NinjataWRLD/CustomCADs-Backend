using CustomCADs.Catalog.Endpoints.Helpers.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Gallery.Get.Single;

public sealed record GetGalleryProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadDate,
    string CadKey,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CountsDto Counts,
    CategoryResponse Category
);
