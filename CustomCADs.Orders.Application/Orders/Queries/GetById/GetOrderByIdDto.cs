using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public record GetOrderByIdDto(
    OrderId Id,
    string Name,
    string Description,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    OrderStatus OrderStatus,
    Image Image,
    UserId BuyerId,
    UserId? DesignerId,
    CadId? CadId,
    ShipmentId? ShipmentId
);
