using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Shared.Abstractions.Payment;

public interface IPaymentService
{
	string PublicKey { get; }
	Task<PaymentDto> InitializeCartPayment(string paymentMethodId, AccountId buyerId, PurchasedCartId cartId, decimal price, string description, CancellationToken ct = default);
	Task<PaymentDto> InitializeCustomPayment(string paymentMethodId, AccountId buyerId, CustomId customId, decimal price, string description, CancellationToken ct = default);
}
