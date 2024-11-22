using CustomCADs.Shared.Speedy.API.Dtos.Shipment.Primary;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Primary;

public static class Mapper
{
    public static PrimaryShipmentModel ToModel(this PrimaryShipmentDto dto)
        => new(
            Id: dto.Id,
            Type: dto.Type
        );
}
