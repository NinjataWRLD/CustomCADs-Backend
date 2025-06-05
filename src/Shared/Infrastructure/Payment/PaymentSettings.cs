namespace CustomCADs.Shared.Infrastructure.Payment;

public record PaymentSettings(string SecretKey, string PublishableKey)
{
	public PaymentSettings() : this(string.Empty, string.Empty) { }
}
