namespace CustomCADs.Shared.Speedy.Dtos;

public record ShipmentDeliveryDto(
    string Deadline,
    string DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);