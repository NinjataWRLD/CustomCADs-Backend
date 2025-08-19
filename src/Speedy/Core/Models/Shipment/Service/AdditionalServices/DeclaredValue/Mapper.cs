using CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentDeclaredValueAdditionalService;

namespace CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.DeclaredValue;

internal static class Mapper
{
	internal static ShipmentDeclaredValueAdditionalServiceDto ToDto(this ShipmentDeclaredValueAdditionalServiceModel model)
		=> new(
			Amount: model.Amount,
			Fragile: model.Fragile,
			IgnoreIfNotApplicable: model.IgnoreIfNotApplicable
		);

	internal static ShipmentDeclaredValueAdditionalServiceModel ToModel(this ShipmentDeclaredValueAdditionalServiceDto dto)
		=> new(
			Amount: dto.Amount,
			Fragile: dto.Fragile,
			IgnoreIfNotApplicable: dto.IgnoreIfNotApplicable
		);
}
