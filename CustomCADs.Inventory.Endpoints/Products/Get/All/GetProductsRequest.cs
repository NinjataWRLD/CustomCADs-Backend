using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Inventory.Endpoints.Products.Get.All;

public record GetProductsRequest(
    ProductSortingType SortingType = ProductSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    string? Name = default,
    int Page = 1,
    int Limit = 20
);
