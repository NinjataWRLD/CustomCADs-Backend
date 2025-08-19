namespace CustomCADs.Speedy.Http.Dtos.CalculationService;

using ShipmentService.ShipmentAdditionalServices;

/// <remarks>
/// 	Calculation service defines service level agreement for the shipment - when shipment should be picked up, service code to be used, sub-services, etc <
/// </remarks>
/// <param name="PickupDate">
/// 	The date for shipment pickup.
/// 	<br />
/// 	Required: No (default value is today). Could be today or a future date
/// </param>
/// <param name="AutoAdjustPickupDate">
///		To find first available date for pickup starting from pickupDate according to pickup schedule for services
/// 	<br />
/// 	Required: No (default value is false)
/// </param>
/// <param name="ServiceIds">
///		Services for which calculation is requested
/// 	<br />
/// 	Required: Yes. Each service id (code) should be valid for the destination.
/// </param>
/// <param name="AdditionalServices">
///		Defines sub-services (like COD, Declared value, etc.) associated with the shipment.
/// 	<br />
/// 	Required: No. Sub-services may be allowed or forbidden for selected service and/or destination.
/// </param>
/// <param name="DeferredDays">
///		This parameter allows users to specify by how many (business) days they would like to postpone the shipment delivery from the standard term.
/// 	<br />
/// 	Required: No (default value is 0). Allowed values are 0, 1 and 2
/// </param>
/// <param name="SaturdayDelivery">
///		This parameter may adjust delivery date to the first business day, if the standard calculated delivery day is a half-working day. If not specified, system will determine this flag based on configured delivery customer working schedule
/// 	<br />
/// 	Required: No
/// </param>
internal record CalculationServiceDto(
	int[] ServiceIds,
	string? PickupDate,
	bool? AutoAdjustPickupDate,
	ShipmentAdditionalServicesDto? AdditionalServices,
	int? DeferredDays,
	bool? SaturdayDelivery
);
