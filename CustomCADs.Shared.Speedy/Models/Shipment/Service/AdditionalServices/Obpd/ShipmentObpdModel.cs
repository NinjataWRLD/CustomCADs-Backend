namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Obpd;

public record ShipmentObpdModel(
    ObpdOption Option,
    int ReturnShipmentServiceId,
    Payer ReturnShipmentPayer
);
