using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Services.Location.Block;
using CustomCADs.Shared.Speedy.Services.Location.Complex;
using CustomCADs.Shared.Speedy.Services.Location.Country;
using CustomCADs.Shared.Speedy.Services.Location.Office;
using CustomCADs.Shared.Speedy.Services.Location.Poi;
using CustomCADs.Shared.Speedy.Services.Location.PostCode;
using CustomCADs.Shared.Speedy.Services.Location.Site;
using CustomCADs.Shared.Speedy.Services.Location.State;
using CustomCADs.Shared.Speedy.Services.Location.Street;

namespace CustomCADs.Shared.Speedy.Services.Location;

public class LocationService(
    BlockService block,
    ComplexService complex,
    CountryService country,
    OfficeService office,
    PointOfInterestService poi,
    PostCodeService postCode,
    SiteService site,
    StateService state,
    StreetService street
)
{
    public async Task<BlockModel[]> FindBlockAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default)
        => await block.FindAsync(siteId, name, type, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetBlocksAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await block.AllAsync(countryId, account, ct).ConfigureAwait(false);

    public async Task<ComplexModel> GetComplexAsync(AccountModel account, long id, CancellationToken ct = default)
        => await complex.GetAsync(id, account, ct).ConfigureAwait(false);
    
    public async Task<ComplexModel[]> FindComplexAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default)
        => await complex.FindAsync(siteId, name, type, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetComplexesAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await complex.AllAsync(countryId, account, ct).ConfigureAwait(false);

    public async Task<CountryModel> GetCountryAsync(AccountModel account, int id, CancellationToken ct = default)
        => await country.GetAsync(id, account, ct).ConfigureAwait(false);
    
    public async Task<CountryModel[]> FindCountryAsync(AccountModel account, string? name = null, string? isoAlpha2 = null, string? isoAlpha3 = null, CancellationToken ct = default)
        => await country.FindAsync(name, isoAlpha2, isoAlpha3, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetCountriesAsync(AccountModel account, CancellationToken ct = default)
        => await country.AllAsync(account, ct).ConfigureAwait(false);

    public async Task<PointOfInterestModel> GetPointOfInterestAsync(AccountModel account, int id, CancellationToken ct = default)
        => await poi.GetAsync(id, account, ct).ConfigureAwait(false);

    public async Task<PointOfInterestModel[]> FindPointOfInterestAsync(AccountModel account, int siteId, string? name = null, CancellationToken ct = default)
        => await poi.FindAsync(siteId, name, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetPointsOfInterestAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await poi.AllAsync(countryId, account, ct).ConfigureAwait(false);

    public async Task<OfficeModel> GetOfficeAsync(AccountModel account, int id, CancellationToken ct = default)
        => await office.GetAsync(id, account, ct).ConfigureAwait(false);

    public async Task<OfficeModel[]> FindOfficeAsync(AccountModel account, int? countryId = null, long? siteId = null, string? name = null, string? siteName = null, int? limit = null, CancellationToken ct = default)
        => await office.FindAsync(countryId, siteId, name, siteName, limit, account, ct).ConfigureAwait(false);

    public async Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(FindNeaerestOfficeModel model, AccountModel account, CancellationToken ct = default)
        => await office.FindNeaerestAsync(model, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetPostCodesAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await postCode.AllAsync(countryId, account, ct).ConfigureAwait(false);

    public async Task<SiteModel> GetSiteAsync(AccountModel account, long id, CancellationToken ct = default)
        => await site.GetAsync(id, account, ct).ConfigureAwait(false);

    public async Task<SiteModel[]> FindSiteAsync(AccountModel account, int countryId, string? name = null, string? type = null, string? postCode = null, string? municipality = null, string? region = null, CancellationToken ct = default)
        => await site.FindAsync(countryId, name, type, postCode, municipality, region, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetSitesAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await site.AllAsync(countryId, account, ct).ConfigureAwait(false);

    public async Task<StateModel> GetStateAsync(AccountModel account, string id, CancellationToken ct = default)
        => await state.GetAsync(id, account, ct).ConfigureAwait(false);

    public async Task<StateModel[]> FindStateAsync(AccountModel account, int countryId, string? name = null, CancellationToken ct = default)
        => await state.FindAsync(countryId, name, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetStatesAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await state.AllAsync(countryId, account, ct).ConfigureAwait(false);

    public async Task<StreetModel> GetStreetAsync(AccountModel account, long id, CancellationToken ct = default)
        => await street.GetAsync(id, account, ct).ConfigureAwait(false);

    public async Task<StreetModel[]> FindStreetAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default)
        => await street.FindAsync(siteId, name, type, account, ct).ConfigureAwait(false);

    public async Task<byte[]> GetStreetsAsync(AccountModel account, int countryId, CancellationToken ct = default)
        => await street.AllAsync(countryId, account, ct).ConfigureAwait(false);
}
