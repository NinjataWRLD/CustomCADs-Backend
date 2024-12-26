using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    DateTime UploadDate,
    ImageDto Image,
    Counts Counts,
    string CreatorName,
    CadDto Cad,
    CategoryDto Category
);

