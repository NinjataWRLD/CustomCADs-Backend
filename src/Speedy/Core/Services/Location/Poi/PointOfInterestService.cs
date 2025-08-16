using CustomCADs.Speedy.Http.Endpoints.LocationEndpoints;
using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Services.Location.Poi;

internal class PointOfInterestService(ILocationEndpoints endpoints)
{
	public async Task<PointOfInterestModel> GetAsync(
		AccountModel account,
		int id,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetPointOfInterestAsync(id, new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.Poi!.ToModel();
	}

	public async Task<PointOfInterestModel[]> FindAsync(
		AccountModel account,
		int siteId,
		string? name,
		CancellationToken ct = default)
	{
		var response = await endpoints.FindPointOfInterestAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			SiteId: siteId,
			Name: name
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return [.. response.Pois?.Select(c => c.ToModel()) ?? []];
	}

	public async Task<byte[]> AllAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetAllPointsOfInterestAsync(countryId, new(
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
