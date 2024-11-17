using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;

public record GetAllCustomOrdersDto(int Count, ICollection<GetAllCustomOrdersItem> CustomOrders);

public record GetAllCustomOrdersItem(
    CustomOrderId Id,
    string Name,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    CustomOrderStatus OrderStatus,
    Image Image
);