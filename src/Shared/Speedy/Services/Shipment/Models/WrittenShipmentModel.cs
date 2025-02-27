namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record WrittenShipmentModel(
    string Id,
    CreatedShipmentParcelModel[] Parcels,
    ShipmentPriceModel Price,
    DateOnly PickupDate,
    DateTime DeliveryDeadline
);