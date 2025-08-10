namespace CustomCADs.Speedy.Services.Models.Shipment.Delivery;

public record ShipmentDeliveryModel(
	DateTime Deadline,
	DateTime? DeliveryDateTime,
	string? Consignee,
	string? DeliveryNote
);
