﻿using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record CartItemDto(
    CartItemId Id,
    int Quantity,
    bool Delivery,
    decimal Price,
    decimal Cost,
    DateTime PurchaseDate,
    ProductId ProductId,
    CartId CartId,
    CadId? CadId,
    ShipmentId? ShipmentId
);