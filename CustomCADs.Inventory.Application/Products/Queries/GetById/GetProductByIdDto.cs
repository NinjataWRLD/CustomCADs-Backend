using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Categories;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

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

