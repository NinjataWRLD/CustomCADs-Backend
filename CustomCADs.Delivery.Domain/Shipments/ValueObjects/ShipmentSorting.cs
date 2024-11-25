﻿using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentSorting(
    ShipmentSortingType Type = ShipmentSortingType.CreationDate,
    SortingDirection Direction = SortingDirection.Descending
);
