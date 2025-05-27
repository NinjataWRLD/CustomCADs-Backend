namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

public record ShipmentRopAdditionalServiceDto(
	ShipmentRopAdditionalServiceLineDto[] Pallets,
	bool? ThirdPartyPayer
);
