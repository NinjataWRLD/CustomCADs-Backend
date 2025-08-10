namespace CustomCADs.Speedy.API.Dtos.CalculationResult;

using Errors;
using ShipmentPrice;
using ShipmentService.ShipmentAdditionalServices;

public record CalculationResultDto(
	int ServiceId,
	ShipmentAdditionalServicesDto? AdditionalServices,
	ShipmentPriceDto Price,
	string PickupDate,
	string DeliveryDeadline,
	ErrorDto? Error
);
