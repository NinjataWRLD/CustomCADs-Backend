using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

public record PostProductRequest(
    string Name, 
    string Description, 
    int CategoryId, 
    MoneyDto Price, 
    IFormFile File, 
    IFormFile Image
);
