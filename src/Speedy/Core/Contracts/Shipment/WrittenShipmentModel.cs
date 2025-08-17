using CustomCADs.Speedy.Core.Models.Shipment.Price;

namespace CustomCADs.Speedy.Core.Contracts.Shipment;

public record WrittenShipmentModel(
	string Id,
	CreatedShipmentParcelModel[] Parcels,
	ShipmentPriceModel Price,
	DateOnly PickupDate,
	DateTime DeliveryDeadline
);
