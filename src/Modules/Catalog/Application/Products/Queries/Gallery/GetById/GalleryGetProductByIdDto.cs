using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Gallery.GetById;

public record GalleryGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    decimal Volume,
    string CreatorName,
    DateTime UploadDate,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    Counts Counts,
    CategoryDto Category
);