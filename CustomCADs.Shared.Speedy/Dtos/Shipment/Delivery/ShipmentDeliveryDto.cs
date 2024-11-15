namespace CustomCADs.Shared.Speedy.Dtos.Shipment.Delivery;

public record ShipmentDeliveryDto(
    string Deadline,
    string DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);
