namespace CustomCADs.Speedy.Http.Dtos.Shipment.Secondary;

using ShipmentParcelNumber;

internal record SecondaryShipmentDto(
	string Id,
	ShipmentType Type,
	ShipmentParcelNumberDto[] Parcels,
	string PickupDate,
	int ServiceId,
	bool HasScans
);
