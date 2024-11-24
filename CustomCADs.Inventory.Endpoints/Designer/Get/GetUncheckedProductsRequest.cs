using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Inventory.Endpoints.Designer.Get;

public record GetUncheckedProductsRequest(
    string? Name = null,
    ProductSortingType SortingType = ProductSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
