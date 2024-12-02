using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdDto(
    CartItemId Id,
    int Quantity,
    DeliveryType DeliveryType,
    decimal Price,
    decimal Cost,
    DateTime PurchaseDate,
    ProductId ProductId,
    CartId CartId,
    CadId? CadId,
    ShipmentId? ShipmentId
);