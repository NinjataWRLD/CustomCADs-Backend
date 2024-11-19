using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Endpoints.CustomOrders.GetAll;

public record GetCustomOrdersRequest(
    DeliveryType? DeliveryType = null,
    CustomOrderStatus? OrderStatus = null,
    string? Name = null,
    CustomOrderSortingType SortingType = CustomOrderSortingType.OrderDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
