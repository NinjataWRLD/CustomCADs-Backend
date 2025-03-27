using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Domain.OngoingOrders.ValueObjects;

public record OngoingOrderSorting(
    OngoingOrderSortingType Type = OngoingOrderSortingType.OrderedAt,
    SortingDirection Direction = SortingDirection.Descending
);
