namespace CustomCADs.Speedy.Http.Dtos.ShipmentService;

using ShipmentAdditionalServices;

internal record ShipmentServiceDto(
	int ServiceId,
	string? PickupDate,
	ShipmentAdditionalServicesDto? AdditionalServices,
	bool? SaturdayDelivery,
	bool AutoAdjustPickupDate = false,
	int DefferedValue = 0
);
