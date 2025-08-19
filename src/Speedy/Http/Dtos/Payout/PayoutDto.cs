namespace CustomCADs.Speedy.Http.Dtos.Payout;

/// <param name="Date">
///     Date of the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DocId">
///     Document id for the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DocType">
///     Document type for the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PaymentType">
///     Payment type for the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Payee">
///     Payee for the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Currency">
///     Currency for the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Amount">
///     Amount for the payout.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Details">
///     Details for the payout.
///     <br />
///     Required: Yes. See PayoutDetailsDto for details.
/// </param>
internal record PayoutDto(
	string Date,
	long DocId,
	ProcessingType DocType,
	PaymentType PaymentType,
	string Payee,
	string Currency,
	double Amount,
	PayoutDetailsDto[] Details
);
