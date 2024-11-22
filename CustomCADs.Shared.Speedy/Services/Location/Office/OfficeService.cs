using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Office;

public class OfficeService(ILocationEndpoints endpoints)
{
    public async Task<OfficeModel> GetAsync(
        AccountModel account, 
        int id, 
        CancellationToken ct = default)
    {
        var response = await endpoints.GetOfficeAsync(id, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Office!.ToModel();
    }

    public async Task<OfficeModel[]> FindAsync(
        AccountModel account, 
        int? countryId, 
        long? siteId, 
        string? name, 
        string? siteName, 
        int? limit, 
        CancellationToken ct = default)
    {
        var response = await endpoints.FindOfficeAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Name: name,
            CountryId: countryId,
            SiteId: siteId,
            SiteName: siteName,
            Limit: limit
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Offices?.Select(c => c.ToModel())];
    }

    public async Task<(int Distancce, OfficeModel Office)[]> FindNeaerestAsync(
        AccountModel account, 
        FindNeaerestOfficeModel model, 
        CancellationToken ct = default)
    {
        var response = await endpoints.FindNearestOfficesAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Address: model.Address.ToDto(),
            Distance: model.Distance,
            Limit: model.Limit,
            OfficeType: model.OfficeType,
            OfficeFeatures: model.OfficeFeatures
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Offices?.Select(c => (c.Distance, c.ToModel()))];
    }
}
