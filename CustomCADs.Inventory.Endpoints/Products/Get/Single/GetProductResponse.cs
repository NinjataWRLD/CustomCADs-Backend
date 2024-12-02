using CustomCADs.Inventory.Endpoints.Helpers.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Single;

public record GetProductResponse(
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
