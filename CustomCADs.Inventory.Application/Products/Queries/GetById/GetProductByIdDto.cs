using CustomCADs.Inventory.Application.Common.Dtos;
using CustomCADs.Inventory.Domain.Products.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    DateTime UploadDate,
    Image Image,
    Counts Counts,
    string CreatorName,
    CadDto Cad,
    CategoryDto Category
);

