using CustomCADs.Speedy.Http.Dtos.ShipmentService;
using CustomCADs.Speedy.Core.Services.Models.Shipment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service;

using static Constants;

internal static class Mapper
{
	internal static ShipmentServiceDto ToDto(this ShipmentServiceModel model)
		=> new(
			ServiceId: model.ServiceId,
			PickupDate: model.PickupDate?.ToString(DateFormat),
			SaturdayDelivery: model.SaturdayDelivery,
			AutoAdjustPickupDate: model.AutoAdjustPickupDate,
			DefferedValue: model.DeferredDays,
			AdditionalServices: model.AdditionalServices?.ToDto()
		);

	internal static ShipmentServiceModel ToModel(this ShipmentServiceDto dto)
		=> new(
			ServiceId: dto.ServiceId,
			PickupDate: dto.PickupDate is not null ? DateOnly.Parse(dto.PickupDate) : null,
			SaturdayDelivery: dto.SaturdayDelivery,
			AutoAdjustPickupDate: dto.AutoAdjustPickupDate,
			DeferredDays: dto.DefferedValue,
			AdditionalServices: dto.AdditionalServices?.ToModel()
		);
}
