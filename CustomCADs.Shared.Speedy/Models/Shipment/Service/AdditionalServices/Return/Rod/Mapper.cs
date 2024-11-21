using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Rod;

public static class Mapper
{
    public static ShipmentRodAdditionalServiceDto ToDto(this ShipmentRodAdditionalServiceModel model)
        => new(
            Enabled: model.Enabled,
            Comment: model.Comment,
            ReturnToClientId: model.ReturnToClientId,
            ReturnToOfficeId: model.ReturnToOfficeId,
            ThirdPartyPayer: model.ThirdPartyPayer
        );
}
