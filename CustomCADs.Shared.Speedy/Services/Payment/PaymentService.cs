using CustomCADs.Shared.Speedy.API.Endpoints.PaymentEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Services.Payment.Models;

namespace CustomCADs.Shared.Speedy.Services.Payment;

using static Constants;

public class PaymentService(IPaymentEndpoints payment)
{
    public async Task<PayoutModel[]> Payout((DateTime From, DateTime To, bool IncludeDetails) model, AccountModel account, CancellationToken ct = default)
    {
        var response = await payment.Payout(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            FromDate: model.From.ToString(DateTimeFormat),
            ToDate: model.To.ToString(DateTimeFormat),
            IncludeDetails: model.IncludeDetails
        ), ct).ConfigureAwait(false);

        response.Error?.EnsureNull();
        return [.. response.Payouts.Select(p => p.ToModel())];
    }
}
