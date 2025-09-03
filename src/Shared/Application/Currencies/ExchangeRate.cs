namespace CustomCADs.Shared.Application.Currencies;

public record ExchangeRate(
	DateTimeOffset Date,
	string Currency,
	decimal Rate
);
