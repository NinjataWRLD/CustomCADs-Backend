using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Speedy.Core.Models.Shipment.Service;

public record ShipmentServiceModel(
	int ServiceId,
	DateOnly? PickupDate,
	ShipmentAdditionalServicesModel? AdditionalServices,
	bool? SaturdayDelivery,
	bool AutoAdjustPickupDate = false,
	int DeferredDays = 0
);
