namespace CustomCADs.Shared.Application.Abstractions.Payment;

public interface IPaymentService
{
	Task<PaymentDto> InitializeCartPayment(string paymentMethodId, AccountId buyerId, PurchasedCartId cartId, decimal price, string description, CancellationToken ct = default);
	Task<PaymentDto> InitializeCustomPayment(string paymentMethodId, AccountId buyerId, CustomId customId, decimal price, string description, CancellationToken ct = default);
}
