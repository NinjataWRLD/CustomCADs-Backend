namespace CustomCADs.Speedy.Http.Dtos.Shipment.Delivery;

/// <summary>
/// DTO representing delivery details of a shipment.
/// </summary>
/// <param name="Deadline">Delivery deadline.</param>
/// <param name="DeliveryDateTime">Actual delivery date and time.</param>
/// <param name="Consignee">Consignee information.</param>
/// <param name="DeliveryNote">Additional delivery notes.</param>
internal record ShipmentDeliveryDto(
	string Deadline,
	string? DeliveryDateTime,
	string? Consignee,
	string? DeliveryNote
);
