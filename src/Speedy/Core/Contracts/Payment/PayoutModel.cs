using CustomCADs.Speedy.Core.Services.Payment.Models;

namespace CustomCADs.Speedy.Core.Contracts.Payment;

public record PayoutModel(
	DateOnly Date,
	long DocId,
	ProcessingType DocType,
	PaymentType PaymentType,
	string Payee,
	string Currency,
	double Amount,
	PayoutDetailsModel[] Details
);
