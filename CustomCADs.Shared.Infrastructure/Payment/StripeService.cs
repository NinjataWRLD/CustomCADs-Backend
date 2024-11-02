using CustomCADs.Shared.Core.Payment;
using CustomCADs.Shared.Core.Payment.Dtos;
using Microsoft.Extensions.Options;
using Stripe;

namespace CustomCADs.Shared.Infrastructure.Payment;

public class StripeService(IOptions<PaymentSettings> settings, PaymentIntentService paymentIntentService) : IPaymentService
{
    public string GetPublicKey() => settings.Value.TestPublishableKey;

    public async Task<PaymentResult> CapturePaymentAsync(string paymentIntentId)
    {
        PaymentIntent paymentIntent = await paymentIntentService.CaptureAsync(paymentIntentId).ConfigureAwait(false);
        return new()
        {
            Id = paymentIntent.Id,
            ClientSecret = paymentIntent.ClientSecret,
            Status = paymentIntent.Status,
        };
    }

    public async Task<PaymentResult> InitializePayment(string paymentMethodId, PurchaseInfo purchase)
    {
        StripeConfiguration.ApiKey = settings.Value.TestSecretKey;

        PaymentIntent paymentIntent = await paymentIntentService.CreateAsync(new()
        {
            Amount = Convert.ToInt64(purchase.Price * 100),
            Currency = "USD",
            PaymentMethod = paymentMethodId,
            Confirm = true,
            Description = $"{purchase.Buyer} bought {purchase.Seller}'s {purchase.Product} for {purchase.Price}$.",
            AutomaticPaymentMethods = new()
            {
                Enabled = true,
                AllowRedirects = "never"
            }
        }).ConfigureAwait(false);

        return new()
        {
            Id = paymentIntent.Id,
            ClientSecret = paymentIntent.ClientSecret,
            Status = paymentIntent.Status,
        };
    }
}
