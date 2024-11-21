using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Receipt;

public static class Mapper
{
    public static ShipmentReturnReceiptAdditionalServiceDto ToDto(this ShipmentReturnReceiptAdditionalServiceModel model)
        => new(
            Enabled: model.Enabled,
            ReturnToClientId: model.ReturnToClientId,
            ReturnToOfficeId: model.ReturnToOfficeId,
            ThirdPartyPayer: model.ThirdPartyPayer
        );
}
