namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentCodAdditionalService;

/// <param name="Description">
///     Description of the fiscal receipt item.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="VatGroup">
///     VAT group for the item.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Amount">
///     Amount for the item (without VAT).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="AmountWithVat">
///     Amount for the item (with VAT).
///     <br />
///     Required: Yes.
/// </param>
internal record ShipmentCodFiscalReceiptItemDto(
	string Description,
	string VatGroup,
	double Amount,
	double AmountWithVat
);
