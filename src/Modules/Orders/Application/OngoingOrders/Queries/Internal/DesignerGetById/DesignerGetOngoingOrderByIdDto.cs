using CustomCADs.Orders.Domain.OngoingOrders.Enums;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.DesignerGetById;

public record DesignerGetOngoingOrderByIdDto(
    OngoingOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    string BuyerName,
    OngoingOrderStatus OrderStatus,
    DateTimeOffset OrderedAt
);
