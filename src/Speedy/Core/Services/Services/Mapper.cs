using CustomCADs.Speedy.Http.Dtos.CourierService;
using CustomCADs.Speedy.Http.Dtos.ExtendedCourierService;
using CustomCADs.Speedy.Core.Services.Services.Models;

namespace CustomCADs.Speedy.Core.Services.Services;

internal static class Mapper
{
	internal static CourierServiceModel ToModel(this CourierServiceDto model)
	   => new(
		   Id: model.Id,
		   Name: model.Name,
		   NameEn: model.NameEn,
		   CargoType: model.CargoType,
		   RequireParcelWeight: model.RequireParcelWeight,
		   RequireParcelSize: model.RequireParcelSize,
		   AdditionalServices: model.AdditionalServices.ToModel()
	   );

	internal static AdditionalCourierServicesModel ToModel(this AdditionalCourierServicesDto model)
		=> new(
			CodAllowance: model.Cod.Allowance,
			ObpdAllowance: model.Obpd.Allowance,
			DeclaredValueAllowance: model.DeclaredValue.Allowance,
			FixedTimeDeliveryAllowance: model.FixedTimeDelivery.Allowance,
			SpecialDeliveryAllowance: model.SpecialDelivery.Allowance,
			DeliveryToFloorAllowance: model.DeliveryToFloor.Allowance,
			RodAllowance: model.Rod.Allowance,
			ReturnReceiptAllowance: model.ReturnReceipt.Allowance,
			SwapAllowance: model.Swap.Allowance,
			RopAllowance: model.Rop.Allowance,
			ReturnVoucherAllowance: model.ReturnVoucher.Allowance
		);

	internal static (string Deadline, CourierServiceModel Courier) ToModel(this ExtendedCourierServiceDto model)
		=> (
			Deadline: model.Deadline,
			new(
				Id: model.Id,
				Name: model.Name,
				NameEn: model.NameEn,
				CargoType: model.CargoType,
				RequireParcelWeight: model.RequireParcelWeight,
				RequireParcelSize: model.RequireParcelSize,
				AdditionalServices: model.AdditionalServices.ToModel()
			)
		);

}
