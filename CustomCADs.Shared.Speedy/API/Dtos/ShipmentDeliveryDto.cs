namespace CustomCADs.Shared.Speedy.API.Dtos;

public record ShipmentDeliveryDto(
    string Deadline,
    string DeliveryDateTime,
    string Consignee,
    string DeliveryNote
);