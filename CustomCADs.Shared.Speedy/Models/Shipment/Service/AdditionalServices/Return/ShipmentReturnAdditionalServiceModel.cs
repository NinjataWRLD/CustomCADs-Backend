using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Receipt;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Rod;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Rop;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Swap;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Voucher;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return;

public record ShipmentReturnAdditionalServiceModel(
    long? SendBackClientId,
    ShipmentRodAdditionalServiceModel? Rod,
    ShipmentReturnReceiptAdditionalServiceModel? ReturnReceipt,
    ShipmentElectronicReturnReceiptAdditionalServiceModel? ElectronicReturnReceipt,
    ShipmentSwapAdditionalServiceModel? Swap,
    ShipmentRopAdditionalServiceModel? Rop,
    ShipmentReturnVoucherAdditionalServiceModel? ReturnVoucher
);