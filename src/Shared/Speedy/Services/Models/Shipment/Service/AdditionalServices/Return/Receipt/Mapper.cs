﻿using CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Return.Receipt;

internal static class Mapper
{
	internal static ShipmentReturnReceiptAdditionalServiceDto ToDto(this ShipmentReturnReceiptAdditionalServiceModel model)
		=> new(
			Enabled: model.Enabled,
			ReturnToClientId: model.ReturnToClientId,
			ReturnToOfficeId: model.ReturnToOfficeId,
			ThirdPartyPayer: model.ThirdPartyPayer
		);

	internal static ShipmentReturnReceiptAdditionalServiceModel ToModel(this ShipmentReturnReceiptAdditionalServiceDto dto)
		=> new(
			Enabled: dto.Enabled,
			ReturnToClientId: dto.ReturnToClientId,
			ReturnToOfficeId: dto.ReturnToOfficeId,
			ThirdPartyPayer: dto.ThirdPartyPayer
		);
}
