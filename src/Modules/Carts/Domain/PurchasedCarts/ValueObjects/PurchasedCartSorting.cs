using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;

public record PurchasedCartSorting(
	PurchasedCartSortingType Type = PurchasedCartSortingType.PurchasedAt,
	SortingDirection Direction = SortingDirection.Descending
);
