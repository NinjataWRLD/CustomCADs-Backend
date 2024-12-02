using CustomCADs.Shared.Speedy.API.Dtos.Shipment.Primary;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Primary;

internal static class Mapper
{
    internal static PrimaryShipmentModel ToModel(this PrimaryShipmentDto dto)
        => new(
            Id: dto.Id,
            Type: dto.Type
        );
}
