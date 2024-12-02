using CustomCADs.Inventory.Application.Common.Dtos;

namespace CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;

public record DesignerGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CadDto Cad,
    CategoryDto Category
);
