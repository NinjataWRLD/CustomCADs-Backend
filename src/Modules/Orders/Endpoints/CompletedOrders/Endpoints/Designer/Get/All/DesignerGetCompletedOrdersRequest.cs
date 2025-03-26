using CustomCADs.Orders.Domain.CompletedOrders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer.Get.All;

public sealed record DesignerGetCompletedOrdersRequest(
    bool? Delivery = null,
    string? Name = null,
    CompletedOrderSortingType SortingType = CompletedOrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
