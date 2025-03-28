﻿using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Pending;

public sealed record GetPendingOngoingOrdersRequest(
    bool? Delivery = null,
    string? Name = null,
    OngoingOrderSortingType SortingType = OngoingOrderSortingType.OrderedAt,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
