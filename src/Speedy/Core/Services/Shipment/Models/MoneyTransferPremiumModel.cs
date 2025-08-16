namespace CustomCADs.Speedy.Core.Services.Shipment.Models;

public record MoneyTransferPremiumModel(
	double? Amount,
	double? AmountLocal,
	Payer? Payer
);
