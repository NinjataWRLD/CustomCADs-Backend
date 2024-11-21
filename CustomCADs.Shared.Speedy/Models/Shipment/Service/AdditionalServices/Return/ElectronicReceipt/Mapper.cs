using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;

public static class Mapper
{
    public static ShipmentElectronicReturnReceiptAdditionalServiceDto ToDto(this ShipmentElectronicReturnReceiptAdditionalServiceModel model)
        => new(
            RecipientEmails: model.RecipientEmails,
            ThirdPartyPayer: model.ThirdPartyPayer
        );
}
