namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record ShipmentPriceAmountModel(
	double Amount,
	double VatPercent,
	double? Percent
);
