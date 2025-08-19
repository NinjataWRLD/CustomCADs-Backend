namespace CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Obpd;

public record ShipmentObpdModel(
	ObpdOption Option,
	int ReturnShipmentServiceId,
	Payer ReturnShipmentPayer
);
