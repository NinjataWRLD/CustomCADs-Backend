﻿using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Site;

public class SiteService(ILocationEndpoints location)
{
    public async Task<SiteModel> GetAsync(long id, AccountModel account, CancellationToken ct = default)
    {
        var response = await location.GetSiteAsync(id, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Site!.ToModel();
    }

    public async Task<SiteModel[]> FindAsync(int countryId, string? name, string? type, string? postCode, string? municipality, string? region, AccountModel account, CancellationToken ct = default)
    {
        var response = await location.FindSiteAsync(new(
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

    public async Task<byte[]> AllAsync(int countryId, AccountModel account, CancellationToken ct = default)
    {
        var response = await location.GetAllSitesAsync(countryId, new(
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
