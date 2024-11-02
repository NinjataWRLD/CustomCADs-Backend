using CustomCADs.Shared.Core.Payment.Dtos;

namespace CustomCADs.Shared.Core.Payment;

public interface IPaymentService
{
    string GetPublicKey();
    Task<PaymentResult> CapturePaymentAsync(string paymentIntentId);
    Task<PaymentResult> InitializePayment(string paymentMethod, PurchaseInfo purchase);
}
