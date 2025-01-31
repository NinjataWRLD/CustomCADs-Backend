using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

public record GalleryGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    DateTime UploadDate,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    Counts Counts,
    CategoryDto Category
);