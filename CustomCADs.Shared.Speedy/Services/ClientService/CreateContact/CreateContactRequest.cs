﻿namespace CustomCADs.Shared.Speedy.Services.ClientService.CreateContact;

using Dtos.ShipmentSenderAndRecipient.ShipmentAddress;
using Dtos.ShipmentSenderAndRecipient.ShipmentPhoneNumber;

public record CreateContactRequest(
    string UserName,
    string Password,
    string ExternalContactId,
    ShipmentPhoneNumberDto Phone1,
    string ClientName,
    bool PrivatePerson,
    ShipmentAddressDto Address,
    string? Location,
    long? ClientSystemId,
    string? SecretKey,
    ShipmentPhoneNumberDto? Phone2,
    string? ObjectName,
    string? Email
);