namespace CustomCADs.Speedy.Http.Dtos.PickupOrder;

/// <param name="Id">
///     Pickup order id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ShipmentIds">
///     List of shipment ids for the pickup order.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PickupPeriodFrom">
///     Pickup period start time, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="PickupPeriodTo">
///     Pickup period end time, if applicable.
///     <br />
///     Required: No.
/// </param>
internal record PickupOrderDto(
	long Id,
	string[] ShipmentIds,
	string? PickupPeriodFrom,
	string? PickupPeriodTo
);
