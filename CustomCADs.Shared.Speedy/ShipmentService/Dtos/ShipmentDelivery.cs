namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record ShipmentDelivery(
    string Deadline,
    string DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);