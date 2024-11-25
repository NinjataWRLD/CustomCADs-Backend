using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Inventory.Domain.Products.ValueObjects;

public record ProductSorting(
    ProductSortingType Type = ProductSortingType.UploadDate,
    SortingDirection Direction = SortingDirection.Descending
);
