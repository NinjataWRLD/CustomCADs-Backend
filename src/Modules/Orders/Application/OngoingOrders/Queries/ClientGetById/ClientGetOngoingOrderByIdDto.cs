using CustomCADs.Orders.Domain.OngoingOrders.Enums;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;

public record ClientGetOngoingOrderByIdDto(
    OngoingOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    string? DesignerName,
    OngoingOrderStatus OrderStatus,
    DateTime OrderDate
);
