namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

public record DesignerGetAllProductsDto(
    ProductId Id,
    string Name,
    string CreatorName,
    DateTimeOffset UploadedAt,
    CategoryDto Category
);
