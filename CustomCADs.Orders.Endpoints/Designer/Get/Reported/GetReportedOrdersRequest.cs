using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Reported;

public sealed record GetReportedOrdersRequest(
    DeliveryType? DeliveryType = null,
    string? Name = null,
    OrderSortingType SortingType = OrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
