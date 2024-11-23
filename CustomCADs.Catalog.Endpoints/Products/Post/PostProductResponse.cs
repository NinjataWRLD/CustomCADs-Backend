using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Post;

public record PostProductResponse(
    ProductId Id,
    string Name,
    string Description,
    string CreatorName,
    string UploadDate,
    MoneyDto Price,
    string Status,
    CategoryDto Category
);
