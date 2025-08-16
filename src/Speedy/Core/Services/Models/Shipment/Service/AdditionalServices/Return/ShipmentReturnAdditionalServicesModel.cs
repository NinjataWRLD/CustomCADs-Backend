using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Receipt;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rod;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rop;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Swap;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Voucher;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return;

public record ShipmentReturnAdditionalServicesModel(
	long? SendBackClientId,
	ShipmentRodAdditionalServiceModel? Rod,
	ShipmentReturnReceiptAdditionalServiceModel? ReturnReceipt,
	ShipmentElectronicReturnReceiptAdditionalServiceModel? ElectronicReturnReceipt,
	ShipmentSwapAdditionalServiceModel? Swap,
	ShipmentRopAdditionalServiceModel? Rop,
	ShipmentReturnVoucherAdditionalServiceModel? ReturnVoucher
);
