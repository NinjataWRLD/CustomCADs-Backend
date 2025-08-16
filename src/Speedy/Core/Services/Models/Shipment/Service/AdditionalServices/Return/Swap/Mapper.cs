using CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Swap;

internal static class Mapper
{
	internal static ShipmentSwapAdditionalServiceDto ToDto(this ShipmentSwapAdditionalServiceModel model)
		=> new(
			ServiceId: model.ServiceId,
			ParcelsCount: model.ParcelsCount,
			DeclaredValue: model.DeclaredValue,
			Fragile: model.Fragile,
			ReturnToOfficeId: model.ReturnToOfficeId,
			ThirdPartyPayer: model.ThirdPartyPayer
		);

	internal static ShipmentSwapAdditionalServiceModel ToModel(this ShipmentSwapAdditionalServiceDto dto)
		=> new(
			ServiceId: dto.ServiceId,
			ParcelsCount: dto.ParcelsCount,
			DeclaredValue: dto.DeclaredValue,
			Fragile: dto.Fragile,
			ReturnToOfficeId: dto.ReturnToOfficeId,
			ThirdPartyPayer: dto.ThirdPartyPayer
		);
}
