using CustomCADs.Speedy.Core.Models.Shipment.Price;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Speedy.Core.Contracts.Calculation;

public record CalculateModel(
    string Service,
    ShipmentAdditionalServicesModel? AdditionalServices,
    ShipmentPriceModel Price,
    DateOnly PickupDate,
    DateTimeOffset DeliveryDeadline
);
