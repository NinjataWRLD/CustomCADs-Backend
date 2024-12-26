namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsDto(
    ProductId Id,
    string Name,
    string Status,
    DateTime UploadDate,
    ImageDto Image,
    string CreatorName,
    CategoryDto Category
);
