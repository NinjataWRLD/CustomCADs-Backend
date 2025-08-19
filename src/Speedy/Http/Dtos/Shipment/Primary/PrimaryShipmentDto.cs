namespace CustomCADs.Speedy.Http.Dtos.Shipment.Primary;

/// <summary>
/// DTO representing primary shipment details.
/// </summary>
/// <param name="Id">Identifier for the primary shipment.</param>
/// <param name="Type">Type of the shipment.</param>
internal record PrimaryShipmentDto(
	string Id,
	ShipmentType Type
);
