using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Client.Models;

public record ContactModel(
    string ExternalContactId,
    PhoneNumberModel Phone1,
    string ClientName,
    bool PrivatePerson,
    ShipmentAddressModel Address,
    string? SecretKey,
    PhoneNumberModel? Phone2,
    string? ObjectName,
    string? Email
);