using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Voucher;

public static class Mapper
{
    public static ShipmentReturnVoucherAdditionalServiceDto ToDto(this ShipmentReturnVoucherAdditionalServiceModel model)
        => new(
            ServiceId: model.ServiceId,
            Payer: model.Payer,
            ValidityPeriod: model.ValidityPeriod
        );

    public static ShipmentReturnVoucherAdditionalServiceModel ToModel(this ShipmentReturnVoucherAdditionalServiceDto dto)
        => new(
            ServiceId: dto.ServiceId,
            Payer: dto.Payer,
            ValidityPeriod: dto.ValidityPeriod
        );
}
