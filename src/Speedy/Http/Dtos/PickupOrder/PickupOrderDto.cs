namespace CustomCADs.Speedy.Http.Dtos.PickupOrder;

internal record PickupOrderDto(
	long Id,
	string[] ShipmentIds,
	string? PickupPeriodFrom,
	string? PickupPeriodTo
);
