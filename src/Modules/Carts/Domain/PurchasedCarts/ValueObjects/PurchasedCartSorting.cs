using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;

public record PurchasedCartSorting(
    PurchasedCartSortingType Type = PurchasedCartSortingType.PurchasedAt,
    SortingDirection Direction = SortingDirection.Descending
);
