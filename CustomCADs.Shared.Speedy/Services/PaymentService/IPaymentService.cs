using Refit;

namespace CustomCADs.Shared.Speedy.Services.PaymentService;

using Payout;

public interface IPaymentService
{
    [Post("")]
    Task<PayoutResponse> Payout(PayoutRequest request, CancellationToken ct = default);
}
