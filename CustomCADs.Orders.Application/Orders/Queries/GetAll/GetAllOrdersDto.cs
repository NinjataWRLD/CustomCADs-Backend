using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public record GetAllOrdersDto(int Count, ICollection<GetAllOrdersItem> Orders);

public record GetAllOrdersItem(
    OrderId Id,
    string Name,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    OrderStatus OrderStatus,
    Image Image,
    string DesignerName
);