namespace CustomCADs.Speedy.Http.Endpoints.PaymentEndpoints.Payout;

internal record PayoutRequest(
	string UserName,
	string Password,
	string FromDate,
	string ToDate,
	string? Language,
	long? ClientSystemId,
	bool? IncludeDetails
);
