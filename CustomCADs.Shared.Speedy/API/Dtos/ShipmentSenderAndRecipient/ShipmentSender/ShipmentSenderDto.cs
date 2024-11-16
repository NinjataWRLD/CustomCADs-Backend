namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentSender;

using ShipmentAddress;
using ShipmentPhoneNumber;

public record ShipmentSenderDto(
    ShipmentPhoneNumberDto Phone1,
    ShipmentPhoneNumberDto? Phone2,
    ShipmentPhoneNumberDto? Phone3,
    ShipmentAddressDto? Address,
    string? Email,
    long? ClientId,
    string? ClientName,
    string? ContactName,
    bool? PrivatePerson,
    int? DropoffOfficeId,
    int? DropoffGeoPUDOId
);
