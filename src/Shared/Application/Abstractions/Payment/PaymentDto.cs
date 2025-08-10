namespace CustomCADs.Shared.Application.Abstractions.Payment;

public record PaymentDto(
	string ClientSecret,
	string Message
);
