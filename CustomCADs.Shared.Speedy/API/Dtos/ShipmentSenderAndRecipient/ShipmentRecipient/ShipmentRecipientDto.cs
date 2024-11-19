﻿namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.ShipmentRecipient;

using ShipmentAddress;
using ShipmentPhoneNumber;

public record ShipmentRecipientDto(
    ShipmentPhoneNumberDto Phone1,
    string ClientName,
    bool PrivatePerson,
    ShipmentAddressDto Address,
    string? ContactName,
    string? Email
);