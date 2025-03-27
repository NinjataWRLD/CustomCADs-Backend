using CustomCADs.Orders.Domain.CompletedOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.All;

public sealed record GetCompletedOrdersRequest(
    bool? Delivery = null,
    string? Name = null,
    CompletedOrderSortingType SortingType = CompletedOrderSortingType.OrderedAt,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
