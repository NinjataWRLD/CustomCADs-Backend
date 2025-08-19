using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Speedy.Core.Models.Calculation.Service;

public record CalculationServiceModel(
	int[] ServiceIds,
	DateOnly? PickupDate,
	bool? AutoAdjustPickupDate,
	ShipmentAdditionalServicesModel? AdditionalServices,
	int? DeferredDays,
	bool? SaturdayDelivery
);
