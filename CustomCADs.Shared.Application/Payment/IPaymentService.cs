using CustomCADs.Shared.Application.Payment.Dtos;

namespace CustomCADs.Shared.Application.Payment;

public interface IPaymentService
{
    string GetPublicKey();
    Task<PaymentResult> CapturePaymentAsync(string paymentIntentId);
    Task<PaymentResult> InitializePayment(string paymentMethod, PurchaseInfo purchase);
}
