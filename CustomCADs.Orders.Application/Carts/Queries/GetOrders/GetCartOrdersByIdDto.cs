using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Orders.Application.Carts.Queries.GetOrders;

public record GetCartOrdersByIdDto(
    GalleryOrderId Id,
    int Quantity,
    DeliveryType DeliveryType,
    Money Price,
    Money Cost,
    DateTime PurchaseDate,
    ProductId ProductId,
    CartId CartId,
    CadId? CadId,
    ShipmentId? ShipmentId
);