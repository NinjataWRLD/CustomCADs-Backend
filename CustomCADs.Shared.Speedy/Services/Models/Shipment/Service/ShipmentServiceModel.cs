using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;

public record ShipmentServiceModel(
    int ServiceId,
    DateOnly? PickupDate,
    ShipmentAdditionalServicesModel? AdditionalServices,
    bool? SaturdayDelivery,
    bool AutoAdjustPickupDate = false,
    int DeferredDays = 0
);