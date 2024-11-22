using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Complex;

public class ComplexService(ILocationEndpoints endpoints)
{
    public async Task<ComplexModel> GetAsync(
        AccountModel account,
        long id,
        CancellationToken ct = default)
    {
        var response = await endpoints.GetComplexAsync(id, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Complex!.ToModel();
    }

    public async Task<ComplexModel[]> FindAsync(
        AccountModel account,
        int siteId,
        string? name,
        string? type,
        CancellationToken ct = default)
    {
        var response = await endpoints.FindComplexAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            SiteId: siteId,
            Name: name,
            Type: type
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Complexes?.Select(c => c.ToModel())];
    }

    public async Task<byte[]> AllAsync(
        AccountModel account,
        int countryId,
        CancellationToken ct = default)
    {
        var response = await endpoints.GetAllComplexesAsync(countryId, new(
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
