using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Domain.Products.ValueObjects;

public record ProductSorting(
    ProductSortingType Type = ProductSortingType.UploadDate,
    SortingDirection Direction = SortingDirection.Descending
);
