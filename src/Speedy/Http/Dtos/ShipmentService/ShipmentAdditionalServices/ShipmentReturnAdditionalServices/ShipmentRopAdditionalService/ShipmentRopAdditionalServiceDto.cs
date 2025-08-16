namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

internal record ShipmentRopAdditionalServiceDto(
	ShipmentRopAdditionalServiceLineDto[] Pallets,
	bool? ThirdPartyPayer
);
