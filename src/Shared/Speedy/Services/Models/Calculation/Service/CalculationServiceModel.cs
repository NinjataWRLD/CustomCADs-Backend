using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Calculation.Service;

public record CalculationServiceModel(
    int[] ServiceIds,
    DateOnly? PickupDate,
    bool? AutoAdjustPickupDate,
    ShipmentAdditionalServicesModel? AdditionalServices,
    int? DeferredDays,
    bool? SaturdayDelivery
);