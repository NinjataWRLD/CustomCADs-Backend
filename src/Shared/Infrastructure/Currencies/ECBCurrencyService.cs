using CustomCADs.Shared.Application.Currencies;
using CustomCADs.Shared.Infrastructure.Utilities;

namespace CustomCADs.Shared.Infrastructure.Currencies;

public class ECBCurrencyService(HttpClient client) : ICurrencyService
{
	public async Task<IReadOnlyCollection<ExchangeRate>> GetRatesAsync()
	{
		HttpResponseMessage response = await SendRequest().ConfigureAwait(false);

		using Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
		Gesmes.Envelope envelope = stream.DeserializeFromXml<Gesmes.Envelope>();

		return [.. envelope.Cube.TimeCube.ToExchangeRates()];
	}

	private async Task<HttpResponseMessage> SendRequest(string resource = "eurofxref-daily.xml")
	{
		HttpResponseMessage response = await client.GetAsync(
			$"/stats/eurofxref/{resource}"
		).ConfigureAwait(false);

		response.EnsureSuccessStatusCode();
		return response;
	}
}
