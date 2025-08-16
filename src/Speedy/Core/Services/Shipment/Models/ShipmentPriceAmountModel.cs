namespace CustomCADs.Speedy.Core.Services.Shipment.Models;

public record ShipmentPriceAmountModel(
	double Amount,
	double VatPercent,
	double? Percent
);
