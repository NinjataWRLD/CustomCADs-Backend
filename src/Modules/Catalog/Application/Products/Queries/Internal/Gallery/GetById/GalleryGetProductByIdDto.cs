using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

[AddRequestCaching(ExpirationType.Absolute, TimeType.Minute, 1)]
public record GalleryGetProductByIdDto(
	ProductId Id,
	string Name,
	string Description,
	decimal Price,
	decimal Volume,
	string CreatorName,
	string[] Tags,
	DateTimeOffset UploadedAt,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates,
	CountsDto Counts,
	CategoryDto Category
);
