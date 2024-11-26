using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    Money Price,
    string Status,
    Image Image,
    string CreatorName,
    CadDto Cad,
    DateTime UploadDate,
    (CategoryId Id, string Name) Category
);

