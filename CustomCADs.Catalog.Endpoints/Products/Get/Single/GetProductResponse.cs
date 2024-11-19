using CustomCADs.Catalog.Endpoints.Categories;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Get.Single;

public record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    MoneyDto Price,
    string CadPath,
    string UploadDate,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryResponse Category
);
