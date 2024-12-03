using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.All;

public sealed record GetProductsRequest(
    int? CategoryId = default,
    string? Name = default,
    ProductSortingType SortingType = ProductSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
