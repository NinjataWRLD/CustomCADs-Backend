namespace CustomCADs.Shared.Abstractions.Payment;

public record PaymentDto(
    string ClientSecret,
    string Message
);