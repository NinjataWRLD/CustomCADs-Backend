using CustomCADs.Catalog.Endpoints.Common.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

public record GalleryGetProductByIdDto(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    decimal Volume,
    string CreatorName,
    DateTimeOffset UploadedAt,
    CoordinatesDto CamCoordinates,
    CoordinatesDto PanCoordinates,
    CountsDto Counts,
    CategoryDto Category
);