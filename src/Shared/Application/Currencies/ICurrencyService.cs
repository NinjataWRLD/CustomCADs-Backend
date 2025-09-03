namespace CustomCADs.Shared.Application.Currencies;

public interface ICurrencyService
{
	public const string ExchangeRatesCacheKey = "exchange-rates";

	public Task<IReadOnlyCollection<ExchangeRate>> GetRatesAsync();
}
