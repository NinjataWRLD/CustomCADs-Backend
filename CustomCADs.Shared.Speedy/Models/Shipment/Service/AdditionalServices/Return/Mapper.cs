using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Receipt;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Rod;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Rop;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Swap;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Voucher;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return;

public static class Mapper
{
    public static ShipmentReturnAdditionalServicesDto ToDto(this ShipmentReturnAdditionalServiceModel model)
        => new(
            SendBackClientId: model.SendBackClientId,
            Rod: model.Rod?.ToDto(),
            ReturnReceipt: model.ReturnReceipt?.ToDto(),
            ElectronicReturnReceipt: model.ElectronicReturnReceipt?.ToDto(),
            Swap: model.Swap?.ToDto(),
            Rop: model.Rop?.ToDto(),
            ReturnVoucher: model.ReturnVoucher?.ToDto()
        );
}
