namespace CustomCADs.Speedy.Http.Dtos.Shipment.ShipmentParcelNumber;

/// <summary>
/// DTO representing a shipment parcel number.
/// </summary>
/// <param name="Id">Identifier for the parcel.</param>
/// <param name="SeqNo">Sequence number of the parcel.</param>
internal record ShipmentParcelNumberDto(
	string Id,
	int SeqNo
);
