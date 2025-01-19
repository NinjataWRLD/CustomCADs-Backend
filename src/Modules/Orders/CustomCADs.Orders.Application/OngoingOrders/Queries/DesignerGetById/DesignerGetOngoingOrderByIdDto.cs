using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;

public record DesignerGetOngoingOrderByIdDto(
    OngoingOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    OngoingOrderStatus OrderStatus,
    DateTime OrderDate,
    AccountId BuyerId
);
