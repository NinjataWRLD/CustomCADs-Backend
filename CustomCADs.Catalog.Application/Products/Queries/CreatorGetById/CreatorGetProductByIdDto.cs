using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Queries.CreatorGetById;

public record CreatorGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string Status,
    DateTime UploadDate,
    Counts Counts,
    string CreatorName,
    CadId CadId,
    CategoryDto Category
);

