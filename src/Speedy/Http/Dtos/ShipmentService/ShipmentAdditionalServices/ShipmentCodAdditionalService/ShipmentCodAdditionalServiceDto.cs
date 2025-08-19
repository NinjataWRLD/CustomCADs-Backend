namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

/// <param name="Amount">
///     COD base amount.
///     <br />
///     Required: Yes. Validated against maximum allowed for destination.
/// </param>
/// <param name="CurrencyCode">
///     COD currency code.
///     <br />
///     Required: No. Default is destination country currency.
/// </param>
/// <param name="PayoutToThirdParty">
///     If true, COD is paid to third party (not sender).
///     <br />
///     Required: No. Requires third party payer of courier service.
/// </param>
/// <param name="PayoutToLoggedClient">
///     If true, COD is paid to logged client.
///     <br />
///     Required: No.
/// </param>
/// <param name="IncludeShippingPrice">
///     Flag to include shipping price in COD.
///     <br />
///     Required: No.
/// </param>
/// <param name="CardPaymentForbidden">
///     Flag indicating COD/PMT card payment is forbidden.
///     <br />
///     Required: No.
/// </param>
/// <param name="ProcessingType">
///     COD processing type. Allowed: CASH, POSTAL_MONEY_TRANSFER.
///     <br />
///     Required: No. Default is CASH.
/// </param>
/// <param name="FiscalReceiptItems">
///     COD fiscal receipt items. If provided, amount is ignored and total is calculated from items.
///     <br />
///     Required: No. Feature depends on country regulations and client contract.
/// </param>
internal record ShipmentCodAdditionalServiceDto(
	double Amount,
	string CurrencyCode,
	bool? PayoutToThirdParty,
	bool? PayoutToLoggedClient,
	bool? IncludeShippingPrice,
	bool? CardPaymentForbidden,
	ProcessingType? ProcessingType,
	ShipmentCodFiscalReceiptItemDto[]? FiscalReceiptItems
);
