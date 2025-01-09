using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;

public record ClientGetCompletedOrderByIdDto(
    CompletedOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    DateTime OrderDate,
    DateTime PurchaseDate,
    AccountId DesignerId,
    CadId CadId,
    ShipmentId? ShipmentId
);
