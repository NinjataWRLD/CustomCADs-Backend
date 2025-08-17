using CustomCADs.Speedy.Http.Endpoints.LocationEndpoints;

namespace CustomCADs.Speedy.Core.Services.Location.Street;

internal class StreetService(ILocationEndpoints endpoints)
{
	public async Task<StreetModel> GetAsync(
		SpeedyAccount account,
		long id,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetStreetAsync(id, new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Street!.ToModel();
	}

	public async Task<StreetModel[]> FindAsync(
		SpeedyAccount account,
		int siteId,
		string? name,
		string? type,
		CancellationToken ct = default)
	{
		var response = await endpoints.FindStreetAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			SiteId: siteId,
			Name: name,
			Type: type
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return [.. response.Streets?.Select(c => c.ToModel()) ?? []];
	}

	public async Task<byte[]> AllAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetAllStreetsAsync(countryId, new(
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
