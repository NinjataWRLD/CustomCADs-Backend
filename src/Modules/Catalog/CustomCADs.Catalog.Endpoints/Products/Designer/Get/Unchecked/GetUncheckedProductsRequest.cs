using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Unchecked;

public sealed record GetUncheckedProductsRequest(
    int? CategoryId = null,
    string? Name = null,
    ProductSortingType SortingType = ProductSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
