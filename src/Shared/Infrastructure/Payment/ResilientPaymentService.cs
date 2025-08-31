using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Customs;

namespace CustomCADs.Shared.Infrastructure.Payment;

public class ResilientPaymentService(
	IPaymentService inner,
	Polly.IAsyncPolicy policy
) : IPaymentService
{
	public Task<PaymentDto> InitializeCartPayment(string paymentMethodId, AccountId buyerId, PurchasedCartId cartId, decimal price, string description, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.InitializeCartPayment(paymentMethodId, buyerId, cartId, price, description, ct));

	public Task<PaymentDto> InitializeCustomPayment(string paymentMethodId, AccountId buyerId, CustomId customId, decimal price, string description, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.InitializeCustomPayment(paymentMethodId, buyerId, customId, price, description, ct));
}
