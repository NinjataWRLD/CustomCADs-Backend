namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Obpd;

public record ShipmentObpdModel(
    ObpdOption Option,
    int ReturnShipmentServiceId,
    Payer ReturnShipmentPayer
);
