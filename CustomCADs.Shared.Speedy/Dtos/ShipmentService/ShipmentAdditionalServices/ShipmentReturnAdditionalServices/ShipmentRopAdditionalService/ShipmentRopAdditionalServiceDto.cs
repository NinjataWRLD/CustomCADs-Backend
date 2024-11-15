namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices.ShipmentRopAdditionalService;

public record ShipmentRopAdditionalServiceDto(
    ShipmentRopAdditionalServiceLineDto[] Pallets,
    bool? ThirdPartyPayer
);