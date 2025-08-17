using CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices;
using CustomCADs.Speedy.Core.Models.Shipment;
using CustomCADs.Speedy.Core.Models.Shipment.Service;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.DeclaredValue;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Obpd;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Return;

namespace CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices;

internal static class Mapper
{
	internal static ShipmentAdditionalServicesDto ToDto(this ShipmentAdditionalServicesModel model)
		=> new(
			Cod: model.Cod?.ToDto(),
			Obdp: model.Obdp?.ToDto(),
			DeclaredValue: model.DeclaredValue?.ToDto(),
			Returns: model.Returns?.ToDto(),
			FixedTimeDelivery: model.FixedTimeDelivery,
			SpecialDeliveryId: model.SpecialDeliveryId,
			DeliveryToFloor: model.DeliveryToFloor
		);

	internal static ShipmentAdditionalServicesModel ToModel(this ShipmentAdditionalServicesDto dto)
		=> new(
			Cod: dto.Cod?.ToModel(),
			Obdp: dto.Obdp?.ToModel(),
			DeclaredValue: dto.DeclaredValue?.ToModel(),
			Returns: dto.Returns?.ToModel(),
			FixedTimeDelivery: dto.FixedTimeDelivery,
			SpecialDeliveryId: dto.SpecialDeliveryId,
			DeliveryToFloor: dto.DeliveryToFloor
		);
}
