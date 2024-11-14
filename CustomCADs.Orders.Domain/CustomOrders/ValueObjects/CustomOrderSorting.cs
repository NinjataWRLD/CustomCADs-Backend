using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Domain.CustomOrders.ValueObjects;

public record CustomOrderSorting(
    CustomOrderSortingType Type = CustomOrderSortingType.OrderDate,
    SortingDirection Direction = SortingDirection.Descending
);
