using CustomCADs.Orders.Domain.Shipments.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Domain.Shipments.ValueObjects;

public record ShipmentSorting(
    ShipmentSortingType Type = ShipmentSortingType.CreationDate,
    SortingDirection Direction = SortingDirection.Descending
);
