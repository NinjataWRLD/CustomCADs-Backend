namespace CustomCADs.Speedy.API.Dtos.ShipmentPrice;

public record ShipmentPriceAmountDto(
	double Amount,
	double VatPercent,
	double? Percent
);
