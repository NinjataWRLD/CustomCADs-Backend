namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    string CreatorName,
    int Views,
    DateTimeOffset UploadedAt,
    CategoryDto Category
);
