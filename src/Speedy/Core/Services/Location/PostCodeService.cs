using CustomCADs.Speedy.Http.Endpoints.LocationEndpoints;

namespace CustomCADs.Speedy.Core.Services.Location;

internal class PostCodeService(ILocationEndpoints endpoints)
{
	public async Task<byte[]> AllAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default)
	{
		var response = await endpoints.GetAllPostCodesAsync(countryId, new(
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
