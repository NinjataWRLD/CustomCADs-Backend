﻿namespace CustomCADs.Shared.Speedy.Services.PickupService.Pickup;

using Enums;

public record PickupRequest(
    string UserName,
    string Password,
    string VisitEndTime,
    string? Language,
    long? ClientSystemId,
    string? PickupDateTime,
    bool? AutoAdjustPickupDate,
    PickupScope? PickupScope,
    string[]? ExplicitShipmentIdList,
    string? ContactName,
    string? PhoneNumber
);
