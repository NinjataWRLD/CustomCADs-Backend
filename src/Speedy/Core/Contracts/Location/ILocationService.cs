using CustomCADs.Speedy.Core.Services.Location.Block;
using CustomCADs.Speedy.Core.Services.Location.Complex;
using CustomCADs.Speedy.Core.Services.Location.Country;
using CustomCADs.Speedy.Core.Services.Location.Office;
using CustomCADs.Speedy.Core.Services.Location.Poi;
using CustomCADs.Speedy.Core.Services.Location.Site;
using CustomCADs.Speedy.Core.Services.Location.State;
using CustomCADs.Speedy.Core.Services.Location.Street;
using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Contracts.Location;

internal interface ILocationService
{
	Task<BlockModel[]> FindBlockAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<ComplexModel[]> FindComplexAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<CountryModel[]> FindCountryAsync(AccountModel account, string? name = null, string? isoAlpha2 = null, string? isoAlpha3 = null, CancellationToken ct = default);
	Task<OfficeModel[]> FindOfficeAsync(AccountModel account, int? countryId = null, long? siteId = null, string? name = null, string? siteName = null, int? limit = null, CancellationToken ct = default);
	Task<PointOfInterestModel[]> FindPointOfInterestAsync(AccountModel account, int siteId, string? name = null, CancellationToken ct = default);
	Task<SiteModel[]> FindSiteAsync(AccountModel account, int countryId, string? name = null, string? type = null, string? postCode = null, string? municipality = null, string? region = null, CancellationToken ct = default);
	Task<StateModel[]> FindStateAsync(AccountModel account, int countryId, string? name = null, CancellationToken ct = default);
	Task<StreetModel[]> FindStreetAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<byte[]> GetBlocksAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<ComplexModel> GetComplexAsync(AccountModel account, long id, CancellationToken ct = default);
	Task<byte[]> GetComplexesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<byte[]> GetCountriesAsync(AccountModel account, CancellationToken ct = default);
	Task<CountryModel> GetCountryAsync(AccountModel account, int id, CancellationToken ct = default);
	Task<OfficeModel> GetOfficeAsync(AccountModel account, int id, CancellationToken ct = default);
	Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(FindNeaerestOfficeModel model, AccountModel account, CancellationToken ct = default);
	Task<PointOfInterestModel> GetPointOfInterestAsync(AccountModel account, int id, CancellationToken ct = default);
	Task<byte[]> GetPointsOfInterestAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<byte[]> GetPostCodesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<SiteModel> GetSiteAsync(AccountModel account, long id, CancellationToken ct = default);
	Task<byte[]> GetSitesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<StateModel> GetStateAsync(AccountModel account, string id, CancellationToken ct = default);
	Task<byte[]> GetStatesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<StreetModel> GetStreetAsync(AccountModel account, long id, CancellationToken ct = default);
	Task<byte[]> GetStreetsAsync(AccountModel account, int countryId, CancellationToken ct = default);
}
