namespace CustomCADs.Speedy.Core.Models.Shipment.Price;

public record ShipmentPriceModel(
	double Amount,
	double Vat,
	double Total,
	string Currency,
	Dictionary<string, ShipmentPriceAmountModel> Details,
	double AmountLocal,
	double VatLocal,
	double TotalLocal,
	string CurrencyLocal,
	Dictionary<string, ShipmentPriceAmountModel> DetailsLocal,
	int CurrencyExchangeRateUnit,
	double CurrencyExchangeRate,
	ReturnAmountsModel? ReturnAmounts
);
