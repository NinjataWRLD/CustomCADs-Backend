using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Country;

public class CountryService(ILocationEndpoints endpoints)
{
    public async Task<CountryModel> GetAsync(int id, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.GetCountryAsync(id, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Country!.ToModel();
    }

    public async Task<CountryModel[]> FindAsync(string? name, string? isoAlpha2, string? isoAlpha3, AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.FindCountryAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Name: name,
            IsoAlpha2: isoAlpha2,
            IsoAlpha3: isoAlpha3
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Countries?.Select(c => c.ToModel())];
    }

    public async Task<byte[]> AllAsync(AccountModel account, CancellationToken ct = default)
    {
        var response = await endpoints.GetAllCountriesAsync(new(
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
