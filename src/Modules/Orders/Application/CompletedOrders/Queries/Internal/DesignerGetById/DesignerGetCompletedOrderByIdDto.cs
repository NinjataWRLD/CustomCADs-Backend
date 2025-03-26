using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.DesignerGetById;

public record DesignerGetCompletedOrderByIdDto(
    CompletedOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    DateTime OrderDate,
    DateTime PurchaseDate,
    string BuyerName,
    ShipmentId? ShipmentId
);
