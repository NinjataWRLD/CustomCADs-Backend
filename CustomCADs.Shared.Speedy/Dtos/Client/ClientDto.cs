﻿namespace CustomCADs.Shared.Speedy.Dtos.Client;

using ShipmentSenderAndRecipient.ShipmentAddress;

public record ClientDto(
    long ClientId,
    string ClientName,
    string ObjectName,
    string ContactName,
    AddressDto Address,
    string Email,
    bool PrivatePerson
);
