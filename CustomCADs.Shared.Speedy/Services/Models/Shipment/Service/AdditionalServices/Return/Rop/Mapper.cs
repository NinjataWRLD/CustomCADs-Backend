using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Return.Rop;

public static class Mapper
{
    public static ShipmentRopAdditionalServiceDto ToDto(this ShipmentRopAdditionalServiceModel model)
        => new(
            Pallets: [.. model.Pallets.Select(p => new ShipmentRopAdditionalServiceLineDto(p.ServiceId, p.ParcelsCount))],
            ThirdPartyPayer: model.ThirdPartyPayer
        );

    public static ShipmentRopAdditionalServiceModel ToModel(this ShipmentRopAdditionalServiceDto dto)
        => new(
            Pallets: [.. dto.Pallets.Select(p => (p.ServiceId, p.ParcelsCount))],
            ThirdPartyPayer: dto.ThirdPartyPayer
        );
}
