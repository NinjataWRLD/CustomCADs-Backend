namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentObpd;

public record ShipmentObpdDto(
    ObpdOption Option,
    int ReturnShipmentServiceId,
    Payer ReturnShipmentPayer
);