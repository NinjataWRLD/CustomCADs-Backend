using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.GetProduct;

public record GetProductResponse(
    Guid Id, 
    string Name, 
    string Description, 
    decimal Cost, 
    string CadPath, 
    string UploadDate, 
    CoordinatesDto CamCoordinates, 
    CoordinatesDto PanCoordinates, 
    CategoryResponse Category
);
