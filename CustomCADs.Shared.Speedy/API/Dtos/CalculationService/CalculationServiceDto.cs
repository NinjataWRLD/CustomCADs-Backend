namespace CustomCADs.Shared.Speedy.API.Dtos.CalculationService;

using ShipmentService.ShipmentAdditionalServices;

public record CalculationServiceDto(
    int[] ServiceIds,
    string? PickupDate,
    bool? AutoAdjustPickupDate,
    ShipmentAdditionalServicesDto? AdditionalServices,
    int? DeferredDays,
    bool? SaturdayDelivery
);
