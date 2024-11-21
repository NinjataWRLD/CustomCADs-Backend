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
}
