using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Speedy.Core.Services.Models.Calculation.Service;

public record CalculationServiceModel(
	int[] ServiceIds,
	DateOnly? PickupDate,
	bool? AutoAdjustPickupDate,
	ShipmentAdditionalServicesModel? AdditionalServices,
	int? DeferredDays,
	bool? SaturdayDelivery
);
