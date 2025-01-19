using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Payment.Exceptions;
using JasperFx.CodeGeneration.Frames;
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
        switch (message)
        {
            case FailedPayment:
                string retry = await RetryCaptureAsync(paymentIntent.Id, ct).ConfigureAwait(false);

                return retry == SuccessfulPayment
                    ? SuccessfulPayment
                    : throw PaymentFailedException.WithClientSecret(paymentIntent.ClientSecret, retry);

            case ProcessingPayment:
                await WaitForProcessingToResolve(paymentIntent.Id, ct).ConfigureAwait(false);
                return message;
                
            case SuccessfulPayment:
                return message;

            default:
                throw PaymentFailedException.General(message);
        }
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

    private async Task<string> WaitForProcessingToResolve(string id, CancellationToken ct = default)
    {
        const int MaxRetries = 10;
        const int SecondsBetweenRetries = 1;
        const string ErrorMessage = "Payment is still processing after maximum retries.";

        for (int i = 0; i < MaxRetries; i++)
        {
            PaymentIntent intent = await paymentIntentService.GetAsync(id, cancellationToken: ct).ConfigureAwait(false);
            string message = GetMessageFromStatus(intent.Status);

            switch (message)
            {
                case SuccessfulPayment: 
                    return message;
                
                case ProcessingPayment: 
                    await Task.Delay(SecondsBetweenRetries * 1000, ct).ConfigureAwait(false); break;

                default: 
                    throw PaymentFailedException.General(message);
            }
        }

        throw new TimeoutException(ErrorMessage);
    }
}
