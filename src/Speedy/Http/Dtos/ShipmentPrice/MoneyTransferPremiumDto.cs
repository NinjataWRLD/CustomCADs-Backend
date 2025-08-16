namespace CustomCADs.Speedy.Http.Dtos.ShipmentPrice;

internal record MoneyTransferPremiumDto(
	double? Amount,
	double? AmountLocal,
	Payer? Payer
);
