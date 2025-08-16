namespace CustomCADs.Speedy.Http.Dtos.Payout;

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
