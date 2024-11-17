using CustomCADs.Orders.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Domain.Carts.ValueObjects;

public record CartSorting(
    CartSortingType Type = CartSortingType.PurchaseDate,
    SortingDirection Direction = SortingDirection.Descending
);
