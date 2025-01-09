﻿using CustomCADs.Orders.Domain.CompletedOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Domain.CompletedOrders.ValueObjects;

public record CompletedOrderSorting(
    CompletedOrderSortingType Type = CompletedOrderSortingType.OrderDate,
    SortingDirection Direction = SortingDirection.Descending
);
