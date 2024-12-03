using CustomCADs.Catalog.Application.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.DesignerGetById;

public record DesignerGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CadDto Cad,
    CategoryDto Category
);
