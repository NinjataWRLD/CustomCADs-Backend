namespace CustomCADs.Speedy.Http.Dtos.ShipmentPrice;

/// <param name="Amount">
///     Amount due in client currency.
///     <br />
///     Required: No.
/// </param>
/// <param name="AmountLocal">
///     Amount due in local currency.
///     <br />
///     Required: No.
/// </param>
/// <param name="Payer">
///     The payer of money transfer premium. Allowed: SENDER, RECIPIENT, THIRD_PARTY.
///     <br />
///     Required: No.
/// </param>
internal record MoneyTransferPremiumDto(
	double? Amount,
	double? AmountLocal,
	Payer? Payer
);
