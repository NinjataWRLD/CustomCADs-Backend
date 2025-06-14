namespace CustomCADs.Shared.Infrastructure.Payment;

public record PaymentSettings(string SecretKey, string PublishableKey, string WebhookSecret)
{
	public PaymentSettings() : this(string.Empty, string.Empty, string.Empty) { }
}
