using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Swap;

public static class Mapper
{
    public static ShipmentSwapAdditionalServiceDto ToDto(this ShipmentSwapAdditionalServiceModel model)
        => new(
            ServiceId: model.ServiceId,
            ParcelsCount: model.ParcelsCount,
            DeclaredValue: model.DeclaredValue,
            Fragile: model.Fragile,
            ReturnToOfficeId: model.ReturnToOfficeId,
            ThirdPartyPayer: model.ThirdPartyPayer
        );

    public static ShipmentSwapAdditionalServiceModel ToModel(this ShipmentSwapAdditionalServiceDto dto)
        => new(
            ServiceId: dto.ServiceId,
            ParcelsCount: dto.ParcelsCount,
            DeclaredValue: dto.DeclaredValue,
            Fragile: dto.Fragile,
            ReturnToOfficeId: dto.ReturnToOfficeId,
            ThirdPartyPayer: dto.ThirdPartyPayer
        );
}
