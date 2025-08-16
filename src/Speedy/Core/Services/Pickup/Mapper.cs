using CustomCADs.Speedy.Http.Dtos.PickupOrder;

namespace CustomCADs.Speedy.Core.Services.Pickup;

internal static class Mapper
{
	internal static (long Id, string[] ShipmentIds, DateTime? PickupPeriodFrom, DateTime? PickupPeriodTo) ToModel(this PickupOrderDto dto)
		=> (
			Id: dto.Id,
			ShipmentIds: dto.ShipmentIds,
			PickupPeriodFrom: dto.PickupPeriodFrom is not null ? DateTime.Parse(dto.PickupPeriodFrom) : null,
			PickupPeriodTo: dto.PickupPeriodTo is not null ? DateTime.Parse(dto.PickupPeriodTo) : null
		);
}
