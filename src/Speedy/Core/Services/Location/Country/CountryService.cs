using CustomCADs.Speedy.Http.Endpoints.LocationEndpoints;

namespace CustomCADs.Speedy.Core.Services.Location.Country;

internal class CountryService(ILocationEndpoints endpoints)
{
	public async Task<CountryModel> GetAsync(
		SpeedyAccount account,
		int id,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetCountryAsync(id, new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Country!.ToModel();
	}

	public async Task<CountryModel[]> FindAsync(
		SpeedyAccount account,
		string? name,
		string? isoAlpha2,
		string? isoAlpha3,
		CancellationToken ct = default)
	{
		var response = await endpoints.FindCountryAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			Name: name,
			IsoAlpha2: isoAlpha2,
			IsoAlpha3: isoAlpha3
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return [.. response.Countries?.Select(c => c.ToModel()) ?? []];
	}

	public async Task<byte[]> AllAsync(
		SpeedyAccount account,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetAllCountriesAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.EnsureSuccessStatusCode();
		using MemoryStream stream = new();
		await response.Content.CopyToAsync(stream, ct).ConfigureAwait(false);
		return stream.ToArray();
	}
}
