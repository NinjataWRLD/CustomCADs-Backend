namespace CustomCADs.Shared.Infrastructure.Payment;

public record PaymentSettings(string LiveSecretKey, string LivePublishableKey, string TestPublishableKey, string TestSecretKey)
{
    public PaymentSettings() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
}
