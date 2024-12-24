using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints.Calculation;
using CustomCADs.Shared.Speedy.Services.Client;
using CustomCADs.Shared.Speedy.Services.Location;
using CustomCADs.Shared.Speedy.Services.Location.Country;
using CustomCADs.Shared.Speedy.Services.Location.Office;
using CustomCADs.Shared.Speedy.Services.Location.Site;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Services;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

public class CalculationService(
    ICalculationEndpoints endpoints,
    ClientService clientService,
    LocationService locationService,
    ServicesService servicesService
)
{
    public const string PhoneNumber1 = "0884874113";
    public const string PhoneNumber2 = "0885440400";
    public const string Email = "customcads2023@gmail.com";
    public const string PickupCountry = "Bulgaria";
    public const string PickupSite = "Burgas";

    public async Task<(string Service, ShipmentAdditionalServicesModel? AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, DateTime DeliveryDeadline)[]> CalculateAsync(
        AccountModel account,
        int parcelCount,
        Payer payer,
        double totalWeight,
        string country,
        string site,
        CancellationToken ct = default)
    {
        int dropoffCountryId = await GetCountryId(account, country, ct).ConfigureAwait(false);
        int pickupCountryId = await GetCountryId(account, PickupCountry, ct).ConfigureAwait(false);

        long dropoffSiteId = await GetSiteId(account, dropoffCountryId, site, ct);
        long pickupSiteId = await GetSiteId(account, pickupCountryId, PickupSite, ct);

        int dropoffOfficeId = await GetOfficeId(account, dropoffCountryId, dropoffSiteId, ct).ConfigureAwait(false);
        int pickupOfficeId = await GetOfficeId(account, pickupCountryId, pickupSiteId, ct).ConfigureAwait(false);

        long clientId = await clientService.GetOwnClientIdAsync(account, ct).ConfigureAwait(false);
        var services = await servicesService.Services(account, null, ct).ConfigureAwait(false);

        CalculationRequest request = new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Sender: new(
                ClientId: clientId,
                DropoffOfficeId: dropoffOfficeId,
                DropoffGeoPUDOId: null, // forbidden
                AddressLocation: null, // forbidden
                PrivatePerson: null // forbidden
            ),
            Recipient: new(
                ClientId: clientId,
                PickupOfficeId: pickupOfficeId,
                PrivatePerson: null, // forbidden
                AddressLocation: null, // forbidden
                PickupGeoPUDOId: null // forbidden
            ),
            Service: new(
                ServiceIds: [.. services.Select(s => s.Id)],
                PickupDate: null,
                AutoAdjustPickupDate: true,
                AdditionalServices: null,
                SaturdayDelivery: null,
                DeferredDays: null
            ),
            Content: new(
                ParcelsCount: parcelCount,
                TotalWeight: totalWeight,
                Parcels: null,
                Palletized: null,
                Documents: null
            ),
            Payment: new(
                CourierServicePayer: payer,
                DeclaredValuePayer: null,
                PackagePayer: null,
                ThirdPartyClientId: null,
                DiscountCardId: null,
                SenderBankAccount: null,
                AdministrativeFee: null
            )
        );
        var response = await endpoints.Calculation(request, ct).ConfigureAwait(false);

        response.Error.EnsureNull();

        var calculations = response.Calculations.ToList();
        if (!calculations.Any(c => c.Error is null))
            calculations.ForEach(c => c.Error.EnsureNull());

        return [.. calculations.Where(c => c.Error is null).Select(c => c.ToModel(services))];
    }

    private async Task<int> GetCountryId(AccountModel account, string country, CancellationToken ct)
    {
        CountryModel[] countries = await locationService.FindCountryAsync(
            account: account,
            name: country,
            ct: ct
        ).ConfigureAwait(false);

        return countries.First().Id;
    }

    private async Task<long> GetSiteId(AccountModel account, int countryId, string site, CancellationToken ct)
    {
        SiteModel[] sites = await locationService.FindSiteAsync(
            account: account,
            countryId: countryId,
            name: site,
            ct: ct
        ).ConfigureAwait(false);

        return sites.First().Id;
    }

    private async Task<int> GetOfficeId(AccountModel account, int countryId, long siteId, CancellationToken ct)
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
}
