namespace CustomCADs.Speedy.API.Endpoints.PaymentEndpoints.Payout;

public record PayoutRequest(
	string UserName,
	string Password,
	string FromDate,
	string ToDate,
	string? Language,
	long? ClientSystemId,
	bool? IncludeDetails
);
