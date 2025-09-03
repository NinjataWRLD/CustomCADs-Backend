using CustomCADs.Shared.Application.Currencies;

namespace CustomCADs.Shared.Infrastructure.Currencies;

internal static class Mapper
{
	internal static IReadOnlyCollection<ExchangeRate> ToExchangeRates(this Gesmes.CubeTime cubeTime)
		=> [.. cubeTime.Rates.Select(
			cubeRate => new ExchangeRate(
				Date: cubeTime.Time,
				Currency: cubeRate.Currency,
				Rate: cubeRate.Rate
			)
		)];
}
