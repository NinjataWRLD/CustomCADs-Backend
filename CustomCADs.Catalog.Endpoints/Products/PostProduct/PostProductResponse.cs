using CustomCADs.Catalog.Endpoints.Categories;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

public record PostProductResponse(
    ProductId Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    MoneyDto Price,
    string Status,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryResponse Category
);
