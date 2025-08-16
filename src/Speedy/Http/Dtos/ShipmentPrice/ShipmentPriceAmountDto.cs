namespace CustomCADs.Speedy.Http.Dtos.ShipmentPrice;

internal record ShipmentPriceAmountDto(
	double Amount,
	double VatPercent,
	double? Percent
);
