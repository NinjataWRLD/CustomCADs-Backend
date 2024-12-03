using CustomCADs.Carts.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.Carts.Queries.GetItems;

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