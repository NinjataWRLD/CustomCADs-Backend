namespace CustomCADs.Shared.Application.Payment;

public interface IPaymentService
{
    string PublicKey { get; }
    Task<string> InitializePayment(string paymentMethodId, decimal price, string description);
}
