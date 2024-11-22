using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentObpd;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Obpd;

public static class Mapper
{
    public static ShipmentObpdDto ToDto(this ShipmentObpdModel model)
        => new(
            Option: model.Option,
            ReturnShipmentServiceId: model.ReturnShipmentServiceId,
            ReturnShipmentPayer: model.ReturnShipmentPayer
        );

    public static ShipmentObpdModel ToModel(this ShipmentObpdDto dto)
        => new(
            Option: dto.Option,
            ReturnShipmentServiceId: dto.ReturnShipmentServiceId,
            ReturnShipmentPayer: dto.ReturnShipmentPayer
        );
}
