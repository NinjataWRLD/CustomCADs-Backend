using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Contracts.Payment;

internal interface IPaymentService
{
	Task<PayoutModel[]> Payout(AccountModel account, DateTime fromDate, DateTime toDate, bool? includeDetails = null, CancellationToken ct = default);
}
