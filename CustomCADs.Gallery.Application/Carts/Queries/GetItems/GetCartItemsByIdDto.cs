using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;
using CustomCADs.Shared.Core.Common.TypedIds.Shipments;
using CustomCADs.Shared.Core.Common.ValueObjects;

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