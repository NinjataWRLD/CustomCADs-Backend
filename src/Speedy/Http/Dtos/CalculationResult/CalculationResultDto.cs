namespace CustomCADs.Speedy.Http.Dtos.CalculationResult;

using Errors;
using ShipmentPrice;
using ShipmentService.ShipmentAdditionalServices;

internal record CalculationResultDto(
	int ServiceId,
	ShipmentAdditionalServicesDto? AdditionalServices,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
