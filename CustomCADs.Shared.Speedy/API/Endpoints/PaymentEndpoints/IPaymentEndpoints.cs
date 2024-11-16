using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.PaymentEndpoints;

using Payout;

public interface IPaymentEndpoints
{
    [Post("")]
    Task<PayoutResponse> Payout(PayoutRequest request, CancellationToken ct = default);
}
