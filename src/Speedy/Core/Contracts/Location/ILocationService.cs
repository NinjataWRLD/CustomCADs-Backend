using CustomCADs.Speedy.Core.Services.Location.Block;
using CustomCADs.Speedy.Core.Services.Location.Complex;
using CustomCADs.Speedy.Core.Services.Location.Country;
using CustomCADs.Speedy.Core.Services.Location.Office;
using CustomCADs.Speedy.Core.Services.Location.Poi;
using CustomCADs.Speedy.Core.Services.Location.Site;
using CustomCADs.Speedy.Core.Services.Location.State;
using CustomCADs.Speedy.Core.Services.Location.Street;

namespace CustomCADs.Speedy.Core.Contracts.Location;

internal interface ILocationService
{
	Task<BlockModel[]> FindBlockAsync(SpeedyAccount account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<ComplexModel[]> FindComplexAsync(SpeedyAccount account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<CountryModel[]> FindCountryAsync(SpeedyAccount account, string? name = null, string? isoAlpha2 = null, string? isoAlpha3 = null, CancellationToken ct = default);
	Task<OfficeModel[]> FindOfficeAsync(SpeedyAccount account, int? countryId = null, long? siteId = null, string? name = null, string? siteName = null, int? limit = null, CancellationToken ct = default);
	Task<PointOfInterestModel[]> FindPointOfInterestAsync(SpeedyAccount account, int siteId, string? name = null, CancellationToken ct = default);
	Task<SiteModel[]> FindSiteAsync(SpeedyAccount account, int countryId, string? name = null, string? type = null, string? postCode = null, string? municipality = null, string? region = null, CancellationToken ct = default);
	Task<StateModel[]> FindStateAsync(SpeedyAccount account, int countryId, string? name = null, CancellationToken ct = default);
	Task<StreetModel[]> FindStreetAsync(SpeedyAccount account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<byte[]> GetBlocksAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
	Task<ComplexModel> GetComplexAsync(SpeedyAccount account, long id, CancellationToken ct = default);
	Task<byte[]> GetComplexesAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
	Task<byte[]> GetCountriesAsync(SpeedyAccount account, CancellationToken ct = default);
	Task<CountryModel> GetCountryAsync(SpeedyAccount account, int id, CancellationToken ct = default);
	Task<OfficeModel> GetOfficeAsync(SpeedyAccount account, int id, CancellationToken ct = default);
	Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(SpeedyAccount account, FindNeaerestOfficeModel model, CancellationToken ct = default);
	Task<PointOfInterestModel> GetPointOfInterestAsync(SpeedyAccount account, int id, CancellationToken ct = default);
	Task<byte[]> GetPointsOfInterestAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
	Task<byte[]> GetPostCodesAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
	Task<SiteModel> GetSiteAsync(SpeedyAccount account, long id, CancellationToken ct = default);
	Task<byte[]> GetSitesAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
	Task<StateModel> GetStateAsync(SpeedyAccount account, string id, CancellationToken ct = default);
	Task<byte[]> GetStatesAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
	Task<StreetModel> GetStreetAsync(SpeedyAccount account, long id, CancellationToken ct = default);
	Task<byte[]> GetStreetsAsync(SpeedyAccount account, int countryId, CancellationToken ct = default);
}
