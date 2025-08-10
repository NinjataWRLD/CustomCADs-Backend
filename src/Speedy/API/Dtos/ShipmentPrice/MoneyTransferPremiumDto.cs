namespace CustomCADs.Speedy.API.Dtos.ShipmentPrice;

public record MoneyTransferPremiumDto(
	double? Amount,
	double? AmountLocal,
	Payer? Payer
);
