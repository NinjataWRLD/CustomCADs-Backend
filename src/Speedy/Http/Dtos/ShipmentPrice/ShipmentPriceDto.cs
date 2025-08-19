namespace CustomCADs.Speedy.Http.Dtos.ShipmentPrice;

/// <param name="Amount">
///     Total amount (before VAT) in customer’s currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Vat">
///     VAT amount in customer’s currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Total">
///     Total amount (amount + vat) in customer’s currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Currency">
///     Customer currency code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Details">
///     Amounts that form totals in customer’s currency (including net, discounts, surcharges).
///     <br />
///     Required: Yes. Keys are item titles.
/// </param>
/// <param name="AmountLocal">
///     Total amount before VAT in local system currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="VatLocal">
///     VAT in local system currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="TotalLocal">
///     Total amount in local system currency (amountLocal + vatLocal).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CurrencyLocal">
///     Local system currency code.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DetailsLocal">
///     Amounts that form totals in local system currency (including net, discounts, surcharges).
///     <br />
///     Required: Yes. Keys are item titles.
/// </param>
/// <param name="CurrencyExchangeRateUnit">
///     Currency exchange rate unit for customer currency.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CurrencyExchangeRate">
///     Currency exchange rate used for customer currency conversion.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ReturnAmounts">
///     Shipment returns amounts due.
///     <br />
///     Required: No. See ReturnAmountsDto for details.
/// </param>
internal record ShipmentPriceDto(
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
