using CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

namespace CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Return.Rop;

internal static class Mapper
{
	internal static ShipmentRopAdditionalServiceDto ToDto(this ShipmentRopAdditionalServiceModel model)
		=> new(
			Pallets: [.. model.Pallets.Select(p => new ShipmentRopAdditionalServiceLineDto(p.ServiceId, p.ParcelsCount))],
			ThirdPartyPayer: model.ThirdPartyPayer
		);

	internal static ShipmentRopAdditionalServiceModel ToModel(this ShipmentRopAdditionalServiceDto dto)
		=> new(
			Pallets: [.. dto.Pallets.Select(p => (p.ServiceId, p.ParcelsCount))],
			ThirdPartyPayer: dto.ThirdPartyPayer
		);
}
