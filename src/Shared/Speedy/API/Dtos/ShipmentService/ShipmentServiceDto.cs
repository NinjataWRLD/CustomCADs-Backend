namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService;

using ShipmentAdditionalServices;

public record ShipmentServiceDto(
	int ServiceId,
	string? PickupDate,
	ShipmentAdditionalServicesDto? AdditionalServices,
	bool? SaturdayDelivery,
	bool AutoAdjustPickupDate = false,
	int DefferedValue = 0
);
