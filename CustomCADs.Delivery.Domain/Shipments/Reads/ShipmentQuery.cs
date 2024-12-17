﻿using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Domain.Shipments.Reads;

public record ShipmentQuery(
    AccountId? ClientId = null,
    ShipmentStatus? ShipmentStatus = null,
    ShipmentSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
