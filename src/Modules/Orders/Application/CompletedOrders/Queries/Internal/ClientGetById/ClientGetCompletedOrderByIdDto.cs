﻿using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.ClientGetById;

public record ClientGetCompletedOrderByIdDto(
    CompletedOrderId Id,
    string Name,
    string Description,
    bool Delivery,
    string DesignerName,
    DateTimeOffset OrderedAt,
    DateTimeOffset PurchasedAt,
    ShipmentId? ShipmentId
);
