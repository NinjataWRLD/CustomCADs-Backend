﻿using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;

public record ShipmentRecipientModel(
    bool? PrivatePerson,
    string? ContactName,
    string? Email,
    long? ClientId,
    string? ClientName,
    string? ObjectName,
    int? PickupOfficeId,
    string? PickupGeoPUDOIf,
    bool? AutoSelectNearestOffice,
    AutoSelectNearestOfficePolicyModel? AutoSelectNearestOfficePolicy,
    ShipmentAddressModel? Address,
    PhoneNumberModel? Phone1,
    PhoneNumberModel? Phone2,
    PhoneNumberModel? Phone3
);