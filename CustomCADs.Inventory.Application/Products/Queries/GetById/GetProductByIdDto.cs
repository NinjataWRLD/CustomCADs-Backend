using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    Image Image,
    Counts Counts,
    string CreatorName,
    CadDto Cad,
    DateTime UploadDate,
    (CategoryId Id, string Name) Category
);

