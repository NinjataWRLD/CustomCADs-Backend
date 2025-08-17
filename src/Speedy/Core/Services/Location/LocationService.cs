using CustomCADs.Speedy.Core.Contracts.Location;
using CustomCADs.Speedy.Core.Services.Location.Block;
using CustomCADs.Speedy.Core.Services.Location.Complex;
using CustomCADs.Speedy.Core.Services.Location.Country;
using CustomCADs.Speedy.Core.Services.Location.Office;
using CustomCADs.Speedy.Core.Services.Location.Poi;
using CustomCADs.Speedy.Core.Services.Location.PostCode;
using CustomCADs.Speedy.Core.Services.Location.Site;
using CustomCADs.Speedy.Core.Services.Location.State;
using CustomCADs.Speedy.Core.Services.Location.Street;

namespace CustomCADs.Speedy.Core.Services.Location;


internal class LocationService(
	BlockService block,
	ComplexService complex,
	CountryService country,
	OfficeService office,
	PointOfInterestService poi,
	PostCodeService postCode,
	SiteService site,
	StateService state,
	StreetService street
) : ILocationService
{
	public async Task<CountryModel> GetCountryAsync(
		SpeedyAccount account,
		int id,
		CancellationToken ct = default
	) => await country.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<CountryModel[]> FindCountryAsync(
		SpeedyAccount account,
		string? name = null,
		string? isoAlpha2 = null,
		string? isoAlpha3 = null,
		CancellationToken ct = default
	) => await country.FindAsync(account, name, isoAlpha2, isoAlpha3, ct).ConfigureAwait(false);

	public async Task<byte[]> GetCountriesAsync(
		SpeedyAccount account,
		CancellationToken ct = default
	) => await country.AllAsync(account, ct).ConfigureAwait(false);

	public async Task<StateModel> GetStateAsync(
		SpeedyAccount account,
		string id,
		CancellationToken ct = default
	) => await state.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<StateModel[]> FindStateAsync(
		SpeedyAccount account,
		int countryId,
		string? name = null,
		CancellationToken ct = default
	) => await state.FindAsync(account, countryId, name, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStatesAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await state.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<SiteModel> GetSiteAsync(
		SpeedyAccount account,
		long id,
		CancellationToken ct = default
	) => await site.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<SiteModel[]> FindSiteAsync(
		SpeedyAccount account,
		int countryId,
		string? name = null,
		string? type = null,
		string? postCode = null,
		string? municipality = null,
		string? region = null,
		CancellationToken ct = default
	) => await site.FindAsync(account, countryId, name, type, postCode, municipality, region, ct).ConfigureAwait(false);

	public async Task<byte[]> GetSitesAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await site.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<StreetModel> GetStreetAsync(
		SpeedyAccount account,
		long id,
		CancellationToken ct = default
	) => await street.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<StreetModel[]> FindStreetAsync(
		SpeedyAccount account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await street.FindAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStreetsAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await street.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<ComplexModel> GetComplexAsync(
		SpeedyAccount account,
		long id,
		CancellationToken ct = default
	) => await complex.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<ComplexModel[]> FindComplexAsync(
		SpeedyAccount account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await complex.FindAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetComplexesAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await complex.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<BlockModel[]> FindBlockAsync(
		SpeedyAccount account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await block.FindAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetBlocksAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await block.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel> GetPointOfInterestAsync(
		SpeedyAccount account,
		int id,
		CancellationToken ct = default
	) => await poi.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel[]> FindPointOfInterestAsync(
		SpeedyAccount account,
		int siteId,
		string? name = null,
		CancellationToken ct = default
	) => await poi.FindAsync(account, siteId, name, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPointsOfInterestAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await poi.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPostCodesAsync(
		SpeedyAccount account,
		int countryId,
		CancellationToken ct = default
	) => await postCode.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<OfficeModel> GetOfficeAsync(
		SpeedyAccount account,
		int id,
		CancellationToken ct = default
	) => await office.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<OfficeModel[]> FindOfficeAsync(
		SpeedyAccount account,
		int? countryId = null,
		long? siteId = null,
		string? name = null,
		string? siteName = null,
		int? limit = null,
		CancellationToken ct = default
	) => await office.FindAsync(account, countryId, siteId, name, siteName, limit, ct).ConfigureAwait(false);

	public async Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(
		SpeedyAccount account,
		FindNeaerestOfficeModel model,
		CancellationToken ct = default
	) => await office.FindNeaerestAsync(account, model, ct).ConfigureAwait(false);
}
