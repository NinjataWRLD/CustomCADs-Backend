using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.Single;

public sealed record GetGalleryProductResponse(
	Guid Id,
	string Name,
	string Description,
	decimal Price,
	decimal Volume,
	string[] Tags,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CoordinatesDto CamCoordinates,
	CoordinatesDto PanCoordinates,
	CountsDto Counts,
	CategoryDtoResponse Category
);
