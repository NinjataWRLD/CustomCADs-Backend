namespace CustomCADs.Shared.Abstractions.Payment;

public interface IPaymentService
{
    string PublicKey { get; }
    Task<PaymentDto> InitializePayment(string paymentMethodId, decimal price, string description, CancellationToken ct = default);
}
