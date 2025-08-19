namespace CustomCADs.Speedy.Http.Dtos.Shipment.Secondary;

using ShipmentParcelNumber;

/// <summary>
/// DTO representing secondary shipment details.
/// </summary>
/// <param name="Id">Identifier for the secondary shipment.</param>
/// <param name="Type">Type of the shipment.</param>
/// <param name="Parcels">Array of shipment parcel numbers.</param>
/// <param name="PickupDate">Pickup date for the shipment.</param>
/// <param name="ServiceId">Service identifier.</param>
/// <param name="HasScans">Indicates if the shipment has scans.</param>
internal record SecondaryShipmentDto(
	string Id,
	ShipmentType Type,
	ShipmentParcelNumberDto[] Parcels,
	string PickupDate,
	int ServiceId,
	bool HasScans
);
