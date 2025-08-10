using CustomCADs.Speedy.API.Dtos.Shipment.Primary;

namespace CustomCADs.Speedy.Services.Models.Shipment.Primary;

internal static class Mapper
{
	internal static PrimaryShipmentModel ToModel(this PrimaryShipmentDto dto)
		=> new(
			Id: dto.Id,
			Type: dto.Type
		);
}
