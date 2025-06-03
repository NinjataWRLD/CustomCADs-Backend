using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.All;

public sealed record GetProductsRequest(
	int? CategoryId = default,
	string? Name = default,
	ProductCreatorSortingType SortingType = ProductCreatorSortingType.UploadedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
