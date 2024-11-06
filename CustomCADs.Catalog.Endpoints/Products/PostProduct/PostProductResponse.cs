using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

public record PostProductResponse(
    Guid Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    decimal Cost,
    string CadPath,
    string ImagePath,
    string Status,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CategoryResponse Category
);
