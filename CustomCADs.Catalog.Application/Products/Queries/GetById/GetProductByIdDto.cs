using CustomCADs.Catalog.Application.Categories.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    Money Price,
    string Status,
    Image Image,
    string CreatorName,
    CadDto? Cad,
    DateTime UploadDate,
    CategoryReadDto Category
);

