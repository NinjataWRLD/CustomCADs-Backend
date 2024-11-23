using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Shipments;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdDto(
    CartItemId Id,
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