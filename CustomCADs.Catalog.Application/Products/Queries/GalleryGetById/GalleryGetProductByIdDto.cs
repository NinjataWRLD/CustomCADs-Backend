using CustomCADs.Catalog.Application.Common.Dtos;
using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

public record GalleryGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    DateTime UploadDate,
    Counts Counts,
    CadDto Cad,
    CategoryDto Category
);