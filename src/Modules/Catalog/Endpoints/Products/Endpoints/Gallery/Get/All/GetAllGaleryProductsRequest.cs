using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.All;

public sealed record GetAllGaleryProductsRequest(
	int? CategoryId = null,
	Guid[]? TagIds = null,
	string? Name = null,
	ProductGallerySortingType SortingType = ProductGallerySortingType.UploadedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
