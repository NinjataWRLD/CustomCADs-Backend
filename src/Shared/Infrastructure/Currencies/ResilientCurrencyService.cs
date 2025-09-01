using CustomCADs.Shared.Application.Currencies;

namespace CustomCADs.Shared.Infrastructure.Currencies;

public class ResilientCurrencyService(
	ICurrencyService inner,
	Polly.IAsyncPolicy policy
) : ICurrencyService
{
	public Task<IReadOnlyCollection<ExchangeRate>> GetRatesAsync()
		=> policy.ExecuteAsync(() => inner.GetRatesAsync());
}
