namespace CustomCADs.Shared.Speedy.API.Dtos.Shipment.Delivery;

public record ShipmentDeliveryDto(
    string Deadline,
    string DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);
