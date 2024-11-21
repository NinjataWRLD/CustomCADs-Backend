namespace CustomCADs.Shared.Speedy.Models.Shipment.Delivery;

public record ShipmentDeliveryModel(
    string Deadline,
    DateTime DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);