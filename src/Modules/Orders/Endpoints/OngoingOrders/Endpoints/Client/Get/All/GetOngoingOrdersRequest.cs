using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.All;

public sealed record GetOngoingOrdersRequest(
    bool? Delivery = null,
    OngoingOrderStatus? OrderStatus = null,
    string? Name = null,
    OngoingOrderSortingType SortingType = OngoingOrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
