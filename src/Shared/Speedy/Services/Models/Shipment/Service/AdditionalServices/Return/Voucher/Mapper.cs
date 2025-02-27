using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Return.Voucher;

internal static class Mapper
{
    internal static ShipmentReturnVoucherAdditionalServiceDto ToDto(this ShipmentReturnVoucherAdditionalServiceModel model)
        => new(
            ServiceId: model.ServiceId,
            Payer: model.Payer,
            ValidityPeriod: model.ValidityPeriod
        );

    internal static ShipmentReturnVoucherAdditionalServiceModel ToModel(this ShipmentReturnVoucherAdditionalServiceDto dto)
        => new(
            ServiceId: dto.ServiceId,
            Payer: dto.Payer,
            ValidityPeriod: dto.ValidityPeriod
        );
}
