using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.State;

public class StateService(ILocationEndpoints endpoints)
{
    public async Task<StateModel> GetAsync(string id, AccountModel account, CancellationToken ct = default)
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

    public async Task<StateModel[]> FindAsync(int countryId, string? name, AccountModel account, CancellationToken ct = default)
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
        return [.. response.States?.Select(c => c.ToModel())];
    }

    public async Task<byte[]> AllAsync(int countryId, AccountModel account, CancellationToken ct = default)
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
