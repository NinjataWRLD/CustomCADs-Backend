namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    string CreatorName,
    int Views,
    DateTime UploadDate,
    CategoryDto Category
);
