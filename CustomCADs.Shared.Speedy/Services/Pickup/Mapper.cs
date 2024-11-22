using CustomCADs.Shared.Speedy.API.Dtos.PickupOrder;
using CustomCADs.Shared.Speedy.Services.Pickup.Models;

namespace CustomCADs.Shared.Speedy.Services.Pickup;

public static class Mapper
{
    public static PickupOrderModel ToModel(this PickupOrderDto dto)
        => new(
            Id: dto.Id,
            ShipmentIds: dto.ShipmentIds,
            PickupPeriodFrom: dto.PickupPeriodFrom is not null ? DateTime.Parse(dto.PickupPeriodFrom) : null,
            PickupPeriodTo: dto.PickupPeriodTo is not null ? DateTime.Parse(dto.PickupPeriodTo) : null
        );
}
