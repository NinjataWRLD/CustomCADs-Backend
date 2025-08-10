namespace CustomCADs.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentReturnReceiptAdditionalServiceDto(
	bool Enabled,
	long? ReturnToClientId,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);
