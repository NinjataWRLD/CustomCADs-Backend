namespace CustomCADs.Catalog.Application.Products.Queries.Designer.GetById;

public record DesignerGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CategoryDto Category
);
