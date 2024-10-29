using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public record CreateProductDto(
    string Name,
    string Description,
    int CategoryId,
    decimal Cost,
    ProductStatus Status,
    Guid CreatorId
);
