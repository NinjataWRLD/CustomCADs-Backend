namespace CustomCADs.Speedy.Http.Dtos.ShipmentService;

using ShipmentAdditionalServices;

/// <param name="ServiceId">
///     Service id for the shipment.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PickupDate">
///     Pickup date for the shipment (yyyy-MM-dd).
///     <br />
///     Required: No. If not provided, calculated automatically.
/// </param>
/// <param name="AdditionalServices">
///     Additional services for the shipment.
///     <br />
///     Required: No. See ShipmentAdditionalServicesDto for details.
/// </param>
/// <param name="SaturdayDelivery">
///     Saturday delivery flag.
///     <br />
///     Required: No. If true, shipment is delivered on Saturday.
/// </param>
/// <param name="AutoAdjustPickupDate">
///     Flag to auto-adjust pickup date if needed.
///     <br />
///     Required: No. Default is false.
/// </param>
/// <param name="DefferedValue">
///     Deferred value for the shipment.
///     <br />
///     Required: No. Default is 0.
/// </param>
internal record ShipmentServiceDto(
	int ServiceId,
	string? PickupDate,
	ShipmentAdditionalServicesDto? AdditionalServices,
	bool? SaturdayDelivery,
	bool AutoAdjustPickupDate = false,
	int DefferedValue = 0
);
