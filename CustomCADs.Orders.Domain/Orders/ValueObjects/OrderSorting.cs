using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Domain.Orders.ValueObjects;

public record OrderSorting(
    OrderSortingType Type = OrderSortingType.OrderDate,
    SortingDirection Direction = SortingDirection.Descending
);
