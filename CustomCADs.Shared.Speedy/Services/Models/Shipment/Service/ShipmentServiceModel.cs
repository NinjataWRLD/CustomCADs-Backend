using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;

public record ShipmentServiceModel(
    int ServiceId,
    string? PickupDate,
    ShipmentAdditionalServicesModel? AdditionalServices,
    bool? SaturdayDelivery,
    bool AutoAdjustPickupDate = false,
    int DefferedValue = 0
);