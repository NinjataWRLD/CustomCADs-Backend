namespace CustomCADs.Speedy.Core.Services.Shipment.Models;

public record WrittenShipmentModel(
	string Id,
	CreatedShipmentParcelModel[] Parcels,
	ShipmentPriceModel Price,
	DateOnly PickupDate,
	DateTime DeliveryDeadline
);
