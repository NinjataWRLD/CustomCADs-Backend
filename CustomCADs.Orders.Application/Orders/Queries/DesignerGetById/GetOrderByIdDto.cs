using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public record DesignerGetOrderByIdDto(
    OrderId Id,
    string Name,
    string Description,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    OrderStatus OrderStatus,
    UserId BuyerId,
    CadId? CadId,
    ShipmentId? ShipmentId
);
