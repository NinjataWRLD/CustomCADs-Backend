using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Street;

public class StreetService(ILocationEndpoints location)
{
    public async Task<StreetModel> GetAsync(long id, AccountModel account, CancellationToken ct = default)
    {
        var response = await location.GetStreetAsync(id, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Street!.ToModel();
    }

    public async Task<StreetModel[]> FindAsync(int siteId, string? name, string? type, AccountModel account, CancellationToken ct = default)
    {
        var response = await location.FindStreetAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            SiteId: siteId,
            Name: name,
            Type: type
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Streets?.Select(c => c.ToModel())];
    }

    public async Task<byte[]> AllAsync(int countryId, AccountModel account, CancellationToken ct = default)
    {
        var response = await location.GetAllStreetsAsync(countryId, new(
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
