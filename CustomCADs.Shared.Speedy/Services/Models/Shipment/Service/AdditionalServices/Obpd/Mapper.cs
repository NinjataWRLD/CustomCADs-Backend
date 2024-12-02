using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentObpd;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Obpd;

internal static class Mapper
{
    internal static ShipmentObpdDto ToDto(this ShipmentObpdModel model)
        => new(
            Option: model.Option,
            ReturnShipmentServiceId: model.ReturnShipmentServiceId,
            ReturnShipmentPayer: model.ReturnShipmentPayer
        );

    internal static ShipmentObpdModel ToModel(this ShipmentObpdDto dto)
        => new(
            Option: dto.Option,
            ReturnShipmentServiceId: dto.ReturnShipmentServiceId,
            ReturnShipmentPayer: dto.ReturnShipmentPayer
        );
}
