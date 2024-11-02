namespace CustomCADs.Shared.Infrastructure.Payment;

public class PaymentSettings
{
    public required string LiveSecretKey { get; set; }
    public required string LivePublishableKey { get; set; }
    public required string TestPublishableKey { get; set; }
    public required string TestSecretKey { get; set; }
}
