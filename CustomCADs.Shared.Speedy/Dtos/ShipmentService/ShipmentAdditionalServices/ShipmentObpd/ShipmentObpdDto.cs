namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentObpd;

using Enums;

public record ShipmentObpdDto(
    ObpdOption Option,
    int ReturnShipmentServiceId,
    Payer ReturnShipmentPayer
);