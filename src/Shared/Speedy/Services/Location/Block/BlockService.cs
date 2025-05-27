using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Block;

public class BlockService(ILocationEndpoints endpoints)
{
	public async Task<BlockModel[]> FindAsync(
		AccountModel account,
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
		AccountModel account,
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
