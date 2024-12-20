using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;

public record DesignerGetOrderByIdDto(
    OrderId Id,
    string Name,
    string Description,
    DateTime OrderDate,
    bool Delivery,
    OrderStatus OrderStatus,
    AccountId BuyerId,
    CadId? CadId,
    ShipmentId? ShipmentId
);
