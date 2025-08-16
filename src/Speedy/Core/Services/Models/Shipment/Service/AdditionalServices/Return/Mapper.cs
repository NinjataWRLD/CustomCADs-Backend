using CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;
using CustomCADs.Speedy.Core.Services.Models.Shipment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.ElectronicReceipt;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Receipt;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rod;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rop;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Swap;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Voucher;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return;

internal static class Mapper
{
	internal static ShipmentReturnAdditionalServicesDto ToDto(this ShipmentReturnAdditionalServicesModel model)
		=> new(
			SendBackClientId: model.SendBackClientId,
			Rod: model.Rod?.ToDto(),
			ReturnReceipt: model.ReturnReceipt?.ToDto(),
			ElectronicReturnReceipt: model.ElectronicReturnReceipt?.ToDto(),
			Swap: model.Swap?.ToDto(),
			Rop: model.Rop?.ToDto(),
			ReturnVoucher: model.ReturnVoucher?.ToDto()
		);

	internal static ShipmentReturnAdditionalServicesModel ToModel(this ShipmentReturnAdditionalServicesDto dto)
		=> new(
			SendBackClientId: dto.SendBackClientId,
			Rod: dto.Rod?.ToModel(),
			ReturnReceipt: dto.ReturnReceipt?.ToModel(),
			ElectronicReturnReceipt: dto.ElectronicReturnReceipt?.ToModel(),
			Swap: dto.Swap?.ToModel(),
			Rop: dto.Rop?.ToModel(),
			ReturnVoucher: dto.ReturnVoucher?.ToModel()
		);
}
