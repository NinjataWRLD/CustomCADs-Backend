using CustomCADs.Catalog.Application.Products.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Unchecked;

public sealed record GetUncheckedProductsRequest(
	int? CategoryId = null,
	string? Name = null,
	ProductDesignerSortingType SortingType = ProductDesignerSortingType.UploadedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
