namespace CustomCADs.Shared.Speedy.Models.Shipment.Recipient;

public record ShipmentRecipientModel(
    PhoneNumberModel Phone1,
    string ClientName,
    bool PrivatePerson,
    ShipmentAddressModel Address,
    string? ContactName,
    string? Email
);