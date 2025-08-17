using CustomCADs.Speedy.Http.Endpoints.LocationEndpoints;

namespace CustomCADs.Speedy.Core.Services.Location.State;

internal class StateService(ILocationEndpoints endpoints)
{
	public async Task<StateModel> GetAsync(
		SpeedyAccount account,
		string id,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetStateAsync(id, new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return response.State!.ToModel();
	}

	public async Task<StateModel[]> FindAsync(
		SpeedyAccount account,
		int countryId,
		string? name,
		CancellationToken ct = default)
	{
		var response = await endpoints.FindStateAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			CountryId: countryId,
			Name: name
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return [.. response.States?.Select(c => c.ToModel()) ?? []];
	}

	public async Task<byte[]> AllAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetAllStatesAsync(countryId, new(
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
