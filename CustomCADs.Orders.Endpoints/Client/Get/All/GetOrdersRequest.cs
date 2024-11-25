using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Orders.Endpoints.Client.Get.All;

public record GetOrdersRequest(
    DeliveryType? DeliveryType = null,
    OrderStatus? OrderStatus = null,
    string? Name = null,
    OrderSortingType SortingType = OrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
