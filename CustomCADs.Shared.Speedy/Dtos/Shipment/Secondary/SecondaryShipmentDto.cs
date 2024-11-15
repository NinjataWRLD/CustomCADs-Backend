﻿namespace CustomCADs.Shared.Speedy.Dtos.Shipment.Secondary;

using Enums;
using ShipmentParcelNumber;

public record SecondaryShipmentDto(
    string Id,
    ShipmentType Type,
    ShipmentParcelNumberDto[] Parcels,
    string PickupDate,
    int ServiceId,
    bool HasScans
);