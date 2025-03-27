using CustomCADs.Catalog.Endpoints.Products.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.Products;

public sealed record PostProductResponse(
    Guid Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadedAt,
    decimal Price,
    string Status,
    CategoryResponse Category
);
