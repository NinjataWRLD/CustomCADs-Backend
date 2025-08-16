using CustomCADs.Speedy.Http.Dtos.Shipment.Secondary;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Secondary;

internal static class Mapper
{
	internal static SecondaryShipmentModel ToModel(this SecondaryShipmentDto dto)
		=> new(
			Id: dto.Id,
			Type: dto.Type,
			Parcels: [.. dto.Parcels.Select(p => (p.Id, p.SeqNo))],
			PickupDate: DateOnly.Parse(dto.PickupDate),
			ServiceId: dto.ServiceId,
			HasScans: dto.HasScans
		);
}
