namespace CustomCADs.Speedy.Http.Dtos.CalculationResult;

using Errors;
using ShipmentPrice;
using ShipmentService.ShipmentAdditionalServices;

/// <param name="ServiceId">
///     Service id for calculation.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="AdditionalServices">
///     Additional services included in calculation.
///     <br />
///     Required: No. See ShipmentAdditionalServicesDto for details.
/// </param>
/// <param name="Price">
///     Returned price, if customer has access to view shipment amounts.
///     <br />
///     Required: Yes. See ShipmentPriceDto for details.
/// </param>
/// <param name="PickupDate">
///     Pickup date for calculation.
///     <br />
///     Required: Yes. Format: yyyy-MM-dd.
/// </param>
/// <param name="DeliveryDeadline">
///     Deadline for delivery, if available.
///     <br />
///     Required: Yes. Format: yyyy-MM-ddTHH:mm:ssZ.
/// </param>
/// <param name="Error">
///     Response error, if any.
///     <br />
///     Required: No. See ErrorDto for details.
/// </param>
internal record CalculationResultDto(
	int ServiceId,
	ShipmentAdditionalServicesDto? AdditionalServices,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
