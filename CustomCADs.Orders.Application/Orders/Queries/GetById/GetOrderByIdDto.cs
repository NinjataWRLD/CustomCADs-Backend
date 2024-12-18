using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public record GetOrderByIdDto(
    OrderId Id,
    string Name,
    string Description,
    DateTime OrderDate,
    bool Delivery,
    OrderStatus OrderStatus,
    AccountId? DesignerId,
    CadId? CadId,
    ShipmentId? ShipmentId
);
