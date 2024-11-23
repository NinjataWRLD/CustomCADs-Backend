﻿using CustomCADs.Shared.Speedy.API.Dtos.Shipment.Secondary;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Secondary;

public static class Mapper
{
    public static SecondaryShipmentModel ToModel(this SecondaryShipmentDto dto)
        => new(
            Id: dto.Id,
            Type: dto.Type,
            Parcels: [.. dto.Parcels.Select(p => (p.Id, p.SeqNo))],
            PickupDate: DateOnly.Parse(dto.PickupDate),
            ServiceId: dto.ServiceId,
            HasScans: dto.HasScans
        );
}