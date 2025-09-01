namespace CustomCADs.Shared.Application.Currencies;

public interface ICurrencyService
{
	public Task<IReadOnlyCollection<ExchangeRate>> GetRatesAsync();
}
