namespace CustomCADs.Speedy.Http.Endpoints.PaymentEndpoints.Payout;

using Dtos.Payout;

internal record PayoutResponse(
	PayoutDto[] Payouts,
	ErrorDto? Error
);
