using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Site;

public class SiteService(ILocationEndpoints endpoints)
{
    public async Task<SiteModel> GetAsync(
        AccountModel account,
        long id,
        CancellationToken ct = default)
    {
        var response = await endpoints.GetSiteAsync(id, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Site!.ToModel();
    }

    public async Task<SiteModel[]> FindAsync(
        AccountModel account,
        int countryId,
        string? name,
        string? type,
        string? postCode,
        string? municipality,
        string? region,
        CancellationToken ct = default)
    {
        var response = await endpoints.FindSiteAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            CountryId: countryId,
            Name: name,
            Type: type,
            PostCode: postCode,
            Municipality: municipality,
            Region: region
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Sites?.Select(c => c.ToModel())];
    }

    public async Task<byte[]> AllAsync(
        AccountModel account,
        int countryId,
        CancellationToken ct = default)
    {
        var response = await endpoints.GetAllSitesAsync(countryId, new(
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
