namespace CustomCADs.Speedy.Http.Dtos.CalculationService;

using ShipmentService.ShipmentAdditionalServices;

internal record CalculationServiceDto(
	int[] ServiceIds,
	string? PickupDate,
	bool? AutoAdjustPickupDate,
	ShipmentAdditionalServicesDto? AdditionalServices,
	int? DeferredDays,
	bool? SaturdayDelivery
);
