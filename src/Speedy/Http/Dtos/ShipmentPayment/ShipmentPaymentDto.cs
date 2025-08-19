namespace CustomCADs.Speedy.Http.Dtos.ShipmentPayment;

/// <param name="CourierServicePayer">
///     Courier service payer.
///     <br />
///     Required: Yes. Allowed values: SENDER, RECIPIENT, THIRD_PARTY.
/// </param>
/// <param name="DeclaredValuePayer">
///     Declared value (extended liability) payer.
///     <br />
///     Required: No. If not provided, CourierServicePayer is assumed.
///     Allowed values: SENDER, RECIPIENT, THIRD_PARTY.
/// </param>
/// <param name="PackagePayer">
///     Package payer.
///     <br />
///     Required: No. If not provided, CourierServicePayer is assumed.
///     Allowed values: SENDER, RECIPIENT, THIRD_PARTY.
/// </param>
/// <param name="ThirdPartyClientId">
///     Third party payer client id.
///     <br />
///     Required: No. Used if any payer is THIRD_PARTY.
/// </param>
/// <param name="DiscountCardId">
///     Discount card for discount calculation.
///     <br />
///     Required: No.
/// </param>
/// <param name="SenderBankAccount">
///     Sender COD payout account information.
///     <br />
///     Required: No. Valid IBAN should be present if provided.
/// </param>
/// <param name="AdministrativeFee">
///     Flag to apply administrative fee on price calculations.
///     <br />
///     Required: No. Usage is enabled/configured in client contract. Ignored if not applicable.
/// </param>
internal record ShipmentPaymentDto(
	Payer CourierServicePayer,
	Payer? DeclaredValuePayer,
	Payer? PackagePayer,
	long? ThirdPartyClientId,
	ShipmentDiscountCardIdDto? DiscountCardId,
	BankAccountDto? SenderBankAccount,
	bool? AdministrativeFee
);
