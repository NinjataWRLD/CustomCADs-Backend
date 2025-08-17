namespace CustomCADs.Speedy.Core.Models.Shipment.Price;

public record ShipmentPriceAmountModel(
	double Amount,
	double VatPercent,
	double? Percent
);
