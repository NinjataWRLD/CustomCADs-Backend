using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;

public record DesignerGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CadDto Cad,
    (CategoryId Id, string Name) Category
);
