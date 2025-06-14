﻿namespace CustomCADs.Shared.Speedy.API.Dtos.Shipment.Secondary;

using ShipmentParcelNumber;

public record SecondaryShipmentDto(
	string Id,
	ShipmentType Type,
	ShipmentParcelNumberDto[] Parcels,
	string PickupDate,
	int ServiceId,
	bool HasScans
);
