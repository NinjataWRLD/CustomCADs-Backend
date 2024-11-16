namespace CustomCADs.Shared.Speedy.API.Dtos.Shipment.Recipient;

using ShipmentSenderAndRecipient.ShipmentAddress;

public record RecipientDto(
    int PickupOfficeId,
    string PickupGeoPUDOId,

    // Copied from Client
    long ClientId,
    string ClientName,
    string ObjectName,
    string ContactName,
    AddressDto Address,
    string Email,
    bool PrivatePerson
);
