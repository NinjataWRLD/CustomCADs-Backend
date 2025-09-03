using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Currencies;

namespace CustomCADs.Presentation;

using static Shared.Endpoints.EndpointsConstants;

public static class ExchangeRatesEndpoint
{
	public static void MapExchangeRatesEndpoint(this IEndpointRouteBuilder app)
	{
		app.MapGet($"api/v1/{Paths.ExchangeRates}", async (ICurrencyService service, ICacheService cache, CancellationToken ct = default) =>
		{
			IReadOnlyCollection<ExchangeRate> rates = await cache.GetOrCreateAsync(
				key: ICurrencyService.ExchangeRatesCacheKey,
				factory: service.GetRatesAsync
			).ConfigureAwait(false) ?? [];

			ExchangeRate[] response = [.. rates];
			return Results.Ok(response);
		})
		.WithTags(Tags[Paths.ExchangeRates])
		.WithSummary("Get Exchange Rates")
		.WithDescription("Updates every 24h");
	}
}
