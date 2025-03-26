using CustomCADs.Orders.Domain.OngoingOrders.Enums;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;

public record GetAllOngoingOrdersDto(
    OngoingOrderId Id,
    string Name,
    bool Delivery,
    OngoingOrderStatus OrderStatus,
    DateTime OrderDate,
    string BuyerName,
    string? DesignerName
);