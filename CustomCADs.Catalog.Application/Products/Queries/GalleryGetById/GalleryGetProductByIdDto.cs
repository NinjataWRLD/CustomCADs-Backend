using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

public record GalleryGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    DateTime UploadDate,
    Counts Counts,
    CadId CadId,
    CategoryDto Category
);