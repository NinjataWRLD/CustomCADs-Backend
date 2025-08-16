namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

internal record ShipmentSwapAdditionalServiceDto(
	int ServiceId,
	int ParcelsCount,
	double? DeclaredValue,
	bool? Fragile,
	int? ReturnToOfficeId,
	bool? ThirdPartyPayer
);
