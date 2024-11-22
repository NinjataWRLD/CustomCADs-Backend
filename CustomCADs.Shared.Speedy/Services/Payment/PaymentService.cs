using CustomCADs.Shared.Speedy.API.Endpoints.PaymentEndpoints;
using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Services.Payment.Models;

namespace CustomCADs.Shared.Speedy.Services.Payment;

using static Constants;

public class PaymentService(IPaymentEndpoints endpoints)
{
    public async Task<PayoutModel[]> Payout(
        AccountModel account,
        DateTime fromDate,
        DateTime toDate,
        bool? includeDetails = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.Payout(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            FromDate: fromDate.ToString(DateTimeFormat),
            ToDate: toDate.ToString(DateTimeFormat),
            IncludeDetails: includeDetails
        ), ct).ConfigureAwait(false);

        response.Error?.EnsureNull();
        return [.. response.Payouts.Select(p => p.ToModel())];
    }
}
