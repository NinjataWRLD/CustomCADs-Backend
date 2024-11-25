using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;
using CustomCADs.Shared.Core.Common.TypedIds.Shipments;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public record GetOrderByIdDto(
    OrderId Id,
    string Name,
    string Description,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    OrderStatus OrderStatus,
    UserId BuyerId,
    UserId? DesignerId,
    CadId? CadId,
    ShipmentId? ShipmentId
);
