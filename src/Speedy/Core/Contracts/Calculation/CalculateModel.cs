using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Speedy.Core.Services.Shipment.Models;

namespace CustomCADs.Speedy.Core.Contracts.Calculation;

public record CalculateModel(
    string Service,
    ShipmentAdditionalServicesModel? AdditionalServices,
    ShipmentPriceModel Price,
    DateOnly PickupDate,
    DateTimeOffset DeliveryDeadline
);
