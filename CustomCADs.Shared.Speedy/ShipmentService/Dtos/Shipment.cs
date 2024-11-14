namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record Shipment(
    string Id,
    Sender Sender,
    Recipient Recipient,
    Service Service,
    Content Content,
    Payment Payment,
    string ShipmentNote,
    string Ref1,
    string Ref2,
    ShipmentPrice Price,
    ShipmentDelivery Delivery,
    PrimaryShipment PrimaryShipment,
    string ReturnShipmentId,
    string RedirectShipmentId,
    bool PendingShipment
);
