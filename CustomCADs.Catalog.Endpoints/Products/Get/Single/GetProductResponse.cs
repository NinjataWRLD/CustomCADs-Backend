using CustomCADs.Catalog.Endpoints.Helpers.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Get.Single;

public sealed record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string CadKey,
    string UploadDate,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CountsDto Counts,
    CategoryResponse Category
);
