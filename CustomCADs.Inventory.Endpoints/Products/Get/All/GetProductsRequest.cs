using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Inventory.Endpoints.Products.Get.All;

public sealed record GetProductsRequest(
    int? CategoryId = default,
    string? Name = default,
    ProductSortingType SortingType = ProductSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
