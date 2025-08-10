using CustomCADs.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Speedy.Services.Models;

namespace CustomCADs.Speedy.Services.Location.PostCode;

public class PostCodeService(ILocationEndpoints endpoints)
{
	public async Task<byte[]> AllAsync(
		AccountModel account,
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
