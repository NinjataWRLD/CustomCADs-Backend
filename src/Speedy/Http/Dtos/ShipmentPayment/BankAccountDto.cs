namespace CustomCADs.Speedy.Http.Dtos.ShipmentPayment;

/// <param name="Iban">
///     IBAN.
///     <br />
///     Required: Yes. Validated according to IBAN standards.
/// </param>
/// <param name="AccountHolder">
///     Bank account holder.
///     <br />
///     Required: Yes.
/// </param>
internal record BankAccountDto(
	string Iban,
	string AccountHolder
);
