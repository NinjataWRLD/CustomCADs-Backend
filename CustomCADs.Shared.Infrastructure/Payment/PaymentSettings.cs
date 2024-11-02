namespace CustomCADs.Shared.Infrastructure.Payment;

public record PaymentSettings(string LiveSecretKey, string LivePublishableKey, string TestPublishableKey, string TestSecretKey);
