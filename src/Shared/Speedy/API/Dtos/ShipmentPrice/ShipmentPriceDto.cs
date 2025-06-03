namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentPrice;

public record ShipmentPriceDto(
	double Amount,
	double Vat,
	double Total,
	string Currency,
	Dictionary<string, ShipmentPriceAmountDto> Details,
	double AmountLocal,
	double VatLocal,
	double TotalLocal,
	string CurrencyLocal,
	Dictionary<string, ShipmentPriceAmountDto> DetailsLocal,
	int CurrencyExchangeRateUnit,
	double CurrencyExchangeRate,
	ReturnAmountsDto? ReturnAmounts
);
