using CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rod;

internal static class Mapper
{
	internal static ShipmentRodAdditionalServiceDto ToDto(this ShipmentRodAdditionalServiceModel model)
		=> new(
			Enabled: model.Enabled,
			Comment: model.Comment,
			ReturnToClientId: model.ReturnToClientId,
			ReturnToOfficeId: model.ReturnToOfficeId,
			ThirdPartyPayer: model.ThirdPartyPayer
		);

	internal static ShipmentRodAdditionalServiceModel ToModel(this ShipmentRodAdditionalServiceDto dto)
		=> new(
			Enabled: dto.Enabled,
			Comment: dto.Comment,
			ReturnToClientId: dto.ReturnToClientId,
			ReturnToOfficeId: dto.ReturnToOfficeId,
			ThirdPartyPayer: dto.ThirdPartyPayer
		);
}
