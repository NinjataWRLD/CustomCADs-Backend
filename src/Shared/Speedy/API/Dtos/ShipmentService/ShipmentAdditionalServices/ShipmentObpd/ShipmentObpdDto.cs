namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentObpd;

public record ShipmentObpdDto(
	ObpdOption Option,
	int ReturnShipmentServiceId,
	Payer ReturnShipmentPayer
);
