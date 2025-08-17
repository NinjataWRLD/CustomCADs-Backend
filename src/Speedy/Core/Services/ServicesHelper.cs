using CustomCADs.Speedy.Core.Contracts.Location;
using CustomCADs.Speedy.Core.Contracts.Services;
using CustomCADs.Speedy.Core.Services.Location.Country;
using CustomCADs.Speedy.Core.Services.Location.Site;
using CustomCADs.Speedy.Core.Services.Location.Street;
using CustomCADs.Speedy.Core.Services.Services.Models;

namespace CustomCADs.Speedy.Core.Services;

internal static class ServicesHelper
{
	internal static async Task<int> GetCountryId(this ILocationService locationService, SpeedyAccount account, string country, CancellationToken ct)
	{
		CountryModel[] countries = await locationService.FindCountryAsync(
			account: account,
			name: country,
			ct: ct
		).ConfigureAwait(false);

		return countries.First().Id;
	}

	internal static async Task<long> GetSiteId(this ILocationService locationService, SpeedyAccount account, int countryId, string site, CancellationToken ct)
	{
		SiteModel[] sites = await locationService.FindSiteAsync(
			account: account,
			countryId: countryId,
			name: site,
			ct: ct
		).ConfigureAwait(false);

		return sites.First().Id;
	}

	internal static async Task<long> GetStreetId(this ILocationService locationService, SpeedyAccount account, long siteId, string street, CancellationToken ct)
	{
		StreetModel[] streets = await locationService.FindStreetAsync(
			account: account,
			siteId: Convert.ToInt32(siteId),
			name: street,
			type: null,
			ct: ct
		).ConfigureAwait(false);

		return streets.First().Id;
	}

	internal static async Task<(int Distance, int OfficeId)> GetOfficeId(this ILocationService locationService, SpeedyAccount account, int countryId, long siteId, long streetId, CancellationToken ct)
	{
		var offices = await locationService.GetOfficeAsync(
			model: new(
				Address: new(
					CountryId: countryId,
					SiteId: siteId,
					StreetId: streetId,
					ComplexId: null,
					SiteType: null,
					StreetType: null,
					ComplexType: null,
					SiteName: null,
					ComplexName: null,
					StreetName: null,
					StreetNo: null,
					BlockNo: null,
					EntranceNo: null,
					FloorNo: null,
					ApartmentNo: null,
					PoiId: null,
					AddressNote: null,
					PostCode: null,
					X: null,
					Y: null
				),
				Distance: null,
				Limit: null,
				OfficeType: null,
				OfficeFeatures: null
			),
			account: account,
			ct: ct
		).ConfigureAwait(false);

		var (Distance, Office) = offices.OrderBy(x => x.Distance).First();
		return (Distance, Office.Id);
	}

	internal static async Task<int> GetServiceId(this IServicesService servicesService, SpeedyAccount account, string service, CancellationToken ct)
	{
		CourierServiceModel[] services = await servicesService.Services(
			account: account,
			date: null,
			ct: ct
		).ConfigureAwait(false);

		return services.First(s => s.Name == service || s.NameEn == service).Id;
	}
}
