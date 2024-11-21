using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Calculation.Service;

public record CalculationServiceModel(
    int[] ServiceIds,
    DateOnly? PickupDate,
    bool? AutoAdjustPickupDate,
    ShipmentAdditionalServicesModel? AdditionalServices,
    int? DeferredDays,
    bool? SaturdayDelivery
);