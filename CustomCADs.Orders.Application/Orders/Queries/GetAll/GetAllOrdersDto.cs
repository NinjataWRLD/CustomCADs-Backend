using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public record GetAllOrdersDto(int Count, ICollection<GetAllOrdersItem> Orders);

public record GetAllOrdersItem(
    OrderId Id,
    string Name,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    OrderStatus OrderStatus,
    string BuyerName,
    string? DesignerName
);