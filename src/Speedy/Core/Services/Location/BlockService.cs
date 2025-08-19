using CustomCADs.Speedy.Core.Contracts.Location;
using CustomCADs.Speedy.Http.Endpoints.LocationEndpoints;

namespace CustomCADs.Speedy.Core.Services.Location;

internal class BlockService(ILocationEndpoints endpoints)
{
	public async Task<BlockModel[]> FindAsync(
		SpeedyAccount account,
		int siteId,
		string? name,
		string? type,
		CancellationToken ct = default)
	{
		var response = await endpoints.FindBlockAsync(new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			SiteId: siteId,
			Name: name,
			Type: type
		), ct).ConfigureAwait(false);

		response.Error.EnsureNull();
		return [.. response.Blocks?.Select(b => b.ToModel()) ?? []];
	}

	public async Task<byte[]> AllAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetAllBlocksAsync(countryId, new(
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
