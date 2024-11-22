namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Delivery;

public record ShipmentDeliveryModel(
    string Deadline,
    DateTime DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);