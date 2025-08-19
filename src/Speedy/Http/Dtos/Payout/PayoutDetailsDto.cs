namespace CustomCADs.Speedy.Http.Dtos.Payout;

/// <param name="LineNo">
///     Line number in the payout details.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ShipmentId">
///     Shipment id for the payout detail.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PickupDate">
///     Pickup date for the shipment.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PrimaryShipmentPickupDate">
///     Pickup date for the primary shipment.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DeliveryDate">
///     Delivery date for the shipment.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Sender">
///     Sender name for the payout detail.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Recipient">
///     Recipient name for the payout detail.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Note">
///     Note for the payout detail, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Ref1">
///     Reference 1 for the payout detail, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Ref2">
///     Reference 2 for the payout detail, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="Currency">
///     Currency for the payout detail.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Order">
///     Order number for the payout detail.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Amount">
///     Amount for the payout detail.
///     <br />
///     Required: Yes.
/// </param>
internal record PayoutDetailsDto(
	int LineNo,
	string ShipmentId,
	string PickupDate,
	string PrimaryShipmentPickupDate,
	string DeliveryDate,
	string Sender,
	string Recipient,
	string Note,
	string Ref1,
	string Ref2,
	string Currency,
	long Order,
	double Amount
);
