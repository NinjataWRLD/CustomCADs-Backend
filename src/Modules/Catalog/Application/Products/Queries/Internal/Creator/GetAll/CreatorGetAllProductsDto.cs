namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

public record CreatorGetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    int Views,
    DateTimeOffset UploadedAt,
    CategoryDto Category
);
