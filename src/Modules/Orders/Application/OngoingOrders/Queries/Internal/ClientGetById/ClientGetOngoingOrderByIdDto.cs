using CustomCADs.Orders.Domain.OngoingOrders.Enums;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.ClientGetById;

public record ClientGetOngoingOrderByIdDto(
    OngoingOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    string? DesignerName,
    OngoingOrderStatus OrderStatus,
    DateTimeOffset OrderedAt
);
