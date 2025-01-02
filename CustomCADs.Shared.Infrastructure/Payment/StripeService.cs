using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Payment.Exceptions;
using Microsoft.Extensions.Options;
using Stripe;

namespace CustomCADs.Shared.Infrastructure.Payment;

using static Messages;

public sealed class StripeService(IOptions<PaymentSettings> settings, PaymentIntentService paymentIntentService) : IPaymentService
{
    public string PublicKey => settings.Value.TestPublishableKey;

    public async Task<string> InitializePayment(string paymentMethodId, decimal price, string description, CancellationToken ct = default)
    {
        StripeConfiguration.ApiKey = settings.Value.TestSecretKey;

        PaymentIntent paymentIntent = await paymentIntentService.CreateAsync(new()
        {
            Amount = Convert.ToInt64(price * 100),
            Currency = "USD",
            PaymentMethod = paymentMethodId,
            Confirm = true,
            Description = description,
            AutomaticPaymentMethods = new()
            {
                Enabled = true,
                AllowRedirects = "never"
            }
        }, cancellationToken: ct).ConfigureAwait(false);

        string message = GetMessageFromStatus(paymentIntent.Status);

        if (message == FailedPayment)
        {
            string retry = await RetryCaptureAsync(paymentIntent.Id, ct).ConfigureAwait(false);

            return retry == SuccessfulPayment
                ? SuccessfulPayment
                : throw PaymentFailedException.WithClientSecret(paymentIntent.ClientSecret, retry);
        }

        if (message is not SuccessfulPayment or ProcessingPayment)
        {
            throw PaymentFailedException.General(message);
        }

        return message;
    }

    private static string GetMessageFromStatus(string status)
        => status switch
        {
            "succeeded" => SuccessfulPayment,
            "processing" => ProcessingPayment,
            "canceled" => CanceledPayment,
            "requires_payment_method" => FailedPaymentMethod,
            "requires_action" => FailedPayment,
            "requires_capture" => FailedPaymentCapture,
            _ => string.Format(UnhandledPayment, status)
        };

    private async Task<string> RetryCaptureAsync(string id, CancellationToken ct = default)
        => (await paymentIntentService.CaptureAsync(id, cancellationToken: ct).ConfigureAwait(false)).Status == "succeeded"
                ? SuccessfulPayment
                : FailedPaymentCapture;
}
