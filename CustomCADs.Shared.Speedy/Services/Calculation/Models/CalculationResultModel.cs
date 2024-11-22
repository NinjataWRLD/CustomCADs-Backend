using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation.Models;

public record CalculationResultModel(
    int ServiceId,
    ShipmentAdditionalServicesModel AdditionalServices,
    ShipmentPriceModel Price,
    DateOnly PickupDate,
    string DeliveryDeadline
);
