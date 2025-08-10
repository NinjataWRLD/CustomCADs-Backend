using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Delivery.Domain.Shipments.ValueObjects;

public record ShipmentSorting(
	ShipmentSortingType Type = ShipmentSortingType.RequestedAt,
	SortingDirection Direction = SortingDirection.Descending
);
