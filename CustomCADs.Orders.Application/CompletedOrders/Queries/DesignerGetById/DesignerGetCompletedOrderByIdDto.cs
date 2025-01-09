using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;

public record DesignerGetCompletedOrderByIdDto(
    CompletedOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    DateTime OrderDate,
    DateTime PurchaseDate,
    AccountId BuyerId,
    CadId CadId,
    ShipmentId? ShipmentId
);
