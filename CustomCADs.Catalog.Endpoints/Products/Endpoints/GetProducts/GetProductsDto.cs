using CustomCADs.Catalog.Endpoints.Categories.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.GetProducts;

public record GetProductsDto(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    string ImagePath,
    CategoryResponseDto Category
);
