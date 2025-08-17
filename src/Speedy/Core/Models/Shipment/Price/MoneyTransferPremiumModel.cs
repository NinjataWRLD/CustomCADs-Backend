namespace CustomCADs.Speedy.Core.Models.Shipment.Price;

public record MoneyTransferPremiumModel(
	double? Amount,
	double? AmountLocal,
	Payer? Payer
);
