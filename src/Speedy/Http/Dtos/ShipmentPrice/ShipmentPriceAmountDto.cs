namespace CustomCADs.Speedy.Http.Dtos.ShipmentPrice;

/// <param name="Amount">
///     Shipment price amount. Discounts are negative. Surcharges and net amount are positive.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="VatPercent">
///     VAT percent applicable to the amount. 0.0 means no VAT, 0.2 means 20% VAT.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Percent">
///     Percent field, if associated with the amount. 20% is returned as 0.2.
///     <br />
///     Required: No. Fixed price amounts do not have a percent value.
/// </param>
internal record ShipmentPriceAmountDto(
	double Amount,
	double VatPercent,
	double? Percent
);
