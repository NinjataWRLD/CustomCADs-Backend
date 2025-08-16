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
using CustomCADs.Speedy.Core.Services.Models;

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
		AccountModel account,
		int id,
		CancellationToken ct = default
	) => await country.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<CountryModel[]> FindCountryAsync(
		AccountModel account,
		string? name = null,
		string? isoAlpha2 = null,
		string? isoAlpha3 = null,
		CancellationToken ct = default
	) => await country.FindAsync(account, name, isoAlpha2, isoAlpha3, ct).ConfigureAwait(false);

	public async Task<byte[]> GetCountriesAsync(
		AccountModel account,
		CancellationToken ct = default
	) => await country.AllAsync(account, ct).ConfigureAwait(false);

	public async Task<StateModel> GetStateAsync(
		AccountModel account,
		string id,
		CancellationToken ct = default
	) => await state.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<StateModel[]> FindStateAsync(
		AccountModel account,
		int countryId,
		string? name = null,
		CancellationToken ct = default
	) => await state.FindAsync(account, countryId, name, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStatesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await state.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<SiteModel> GetSiteAsync(
		AccountModel account,
		long id,
		CancellationToken ct = default
	) => await site.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<SiteModel[]> FindSiteAsync(
		AccountModel account,
		int countryId,
		string? name = null,
		string? type = null,
		string? postCode = null,
		string? municipality = null,
		string? region = null,
		CancellationToken ct = default
	) => await site.FindAsync(account, countryId, name, type, postCode, municipality, region, ct).ConfigureAwait(false);

	public async Task<byte[]> GetSitesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await site.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<StreetModel> GetStreetAsync(
		AccountModel account,
		long id,
		CancellationToken ct = default
	) => await street.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<StreetModel[]> FindStreetAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await street.FindAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStreetsAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await street.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<ComplexModel> GetComplexAsync(
		AccountModel account,
		long id,
		CancellationToken ct = default
	) => await complex.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<ComplexModel[]> FindComplexAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await complex.FindAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetComplexesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await complex.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<BlockModel[]> FindBlockAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await block.FindAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetBlocksAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await block.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel> GetPointOfInterestAsync(
		AccountModel account,
		int id,
		CancellationToken ct = default
	) => await poi.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel[]> FindPointOfInterestAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		CancellationToken ct = default
	) => await poi.FindAsync(account, siteId, name, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPointsOfInterestAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await poi.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPostCodesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await postCode.AllAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<OfficeModel> GetOfficeAsync(
		AccountModel account,
		int id,
		CancellationToken ct = default
	) => await office.GetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<OfficeModel[]> FindOfficeAsync(
		AccountModel account,
		int? countryId = null,
		long? siteId = null,
		string? name = null,
		string? siteName = null,
		int? limit = null,
		CancellationToken ct = default
	) => await office.FindAsync(account, countryId, siteId, name, siteName, limit, ct).ConfigureAwait(false);

	public async Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(
		FindNeaerestOfficeModel model,
		AccountModel account,
		CancellationToken ct = default
	) => await office.FindNeaerestAsync(account, model, ct).ConfigureAwait(false);
}
