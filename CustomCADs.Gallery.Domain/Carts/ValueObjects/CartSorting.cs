using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Gallery.Domain.Carts.ValueObjects;

public record CartSorting(
    CartSortingType Type = CartSortingType.PurchaseDate,
    SortingDirection Direction = SortingDirection.Descending
);
