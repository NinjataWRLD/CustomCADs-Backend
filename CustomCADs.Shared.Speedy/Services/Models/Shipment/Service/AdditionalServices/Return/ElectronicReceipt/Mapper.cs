using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;

public static class Mapper
{
    public static ShipmentElectronicReturnReceiptAdditionalServiceDto ToDto(this ShipmentElectronicReturnReceiptAdditionalServiceModel model)
        => new(
            RecipientEmails: model.RecipientEmails,
            ThirdPartyPayer: model.ThirdPartyPayer
        );

    public static ShipmentElectronicReturnReceiptAdditionalServiceModel ToModel(this ShipmentElectronicReturnReceiptAdditionalServiceDto dto)
        => new(
            RecipientEmails: dto.RecipientEmails,
            ThirdPartyPayer: dto.ThirdPartyPayer
        );
}
