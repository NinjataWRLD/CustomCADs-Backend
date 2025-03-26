using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Finished;

public sealed record GetFinishedOngoingOrdersRequest(
    bool? Delivery = null,
    string? Name = null,
    OngoingOrderSortingType SortingType = OngoingOrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
