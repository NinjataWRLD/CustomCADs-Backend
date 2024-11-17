using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.GetById;

public record GetCustomOrderByIdDto(
    CustomOrderId Id,
    string Name,
    string Description,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    CustomOrderStatus OrderStatus,
    Image Image,
    UserId BuyerId,
    UserId? DesignerId,
    CadId? CadId,
    ShipmentId? ShipmentId
);
