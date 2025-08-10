using CustomCADs.Shared.Core.Common.Dtos;

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
