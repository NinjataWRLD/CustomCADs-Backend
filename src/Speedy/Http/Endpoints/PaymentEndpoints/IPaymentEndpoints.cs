using Refit;

namespace CustomCADs.Speedy.Http.Endpoints.PaymentEndpoints;

using Payout;

internal interface IPaymentEndpoints
{
	[Post("/")]
	Task<PayoutResponse> Payout(PayoutRequest request, CancellationToken ct = default);
}
