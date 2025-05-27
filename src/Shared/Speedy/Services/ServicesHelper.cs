using CustomCADs.Shared.Speedy.Services.Location;
using CustomCADs.Shared.Speedy.Services.Location.Country;
using CustomCADs.Shared.Speedy.Services.Location.Office;
using CustomCADs.Shared.Speedy.Services.Location.Site;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Services;
using CustomCADs.Shared.Speedy.Services.Services.Models;

namespace CustomCADs.Shared.Speedy.Services;
public static class ServicesHelper
{
	public static async Task<int> GetCountryId(this LocationService locationService, AccountModel account, string country, CancellationToken ct)
	{
		CountryModel[] countries = await locationService.FindCountryAsync(
			account: account,
			name: country,
			ct: ct
		).ConfigureAwait(false);

		return countries.First().Id;
	}

	public static async Task<long> GetSiteId(this LocationService locationService, AccountModel account, int countryId, string site, CancellationToken ct)
	{
		SiteModel[] sites = await locationService.FindSiteAsync(
			account: account,
			countryId: countryId,
			name: site,
			ct: ct
		).ConfigureAwait(false);

		return sites.First().Id;
	}

	public static async Task<int> GetOfficeId(this LocationService locationService, AccountModel account, int countryId, long siteId, CancellationToken ct)
	{
		OfficeModel[] offices = await locationService.FindOfficeAsync(
			account: account,
			countryId: countryId,
			siteId: siteId,
			name: null,
			siteName: null,
			limit: null,
			ct: ct
		).ConfigureAwait(false);

		return offices.First().Id;
	}

	public static async Task<int> GetServiceId(this ServicesService servicesService, AccountModel account, string service, CancellationToken ct)
	{
		CourierServiceModel[] services = await servicesService.Services(
			account: account,
			date: null,
			ct: ct
		).ConfigureAwait(false);

		return services.First(s => s.Name == service || s.NameEn == service).Id;
	}
}
