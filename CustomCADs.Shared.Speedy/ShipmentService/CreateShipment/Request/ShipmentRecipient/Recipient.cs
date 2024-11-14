namespace CustomCADs.Shared.Speedy.ShipmentService.CreateShipment.Request.ShipmentRecipient;

public record Recipient(
    PhoneNumber Phone1,
    string ClientName,
    bool PrivatePerson,
    Address Address,
    string? ContactName,
    string? Email
);
