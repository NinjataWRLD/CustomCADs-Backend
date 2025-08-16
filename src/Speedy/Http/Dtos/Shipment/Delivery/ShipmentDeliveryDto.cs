namespace CustomCADs.Speedy.Http.Dtos.Shipment.Delivery;

internal record ShipmentDeliveryDto(
	string Deadline,
	string? DeliveryDateTime,
	string? Consignee,
	string? DeliveryNote
);
