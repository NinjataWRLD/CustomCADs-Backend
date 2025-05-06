using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints.Calculation;
using CustomCADs.Shared.Speedy.Services.Client;
using CustomCADs.Shared.Speedy.Services.Location;
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

    public async Task<(string Service, ShipmentAdditionalServicesModel? AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, DateTimeOffset DeliveryDeadline)[]> CalculateAsync(
        AccountModel account,
        int parcelCount,
        Payer payer,
        double totalWeight,
        string country,
        string site,
        CancellationToken ct = default)
    {
        int dropoffCountryId = await locationService.GetCountryId(account, country, ct).ConfigureAwait(false);
        int pickupCountryId = await locationService.GetCountryId(account, PickupCountry, ct).ConfigureAwait(false);

        long dropoffSiteId = await locationService.GetSiteId(account, dropoffCountryId, site, ct).ConfigureAwait(false);
        long pickupSiteId = await locationService.GetSiteId(account, pickupCountryId, PickupSite, ct).ConfigureAwait(false);

        int dropoffOfficeId = await locationService.GetOfficeId(account, dropoffCountryId, dropoffSiteId, ct).ConfigureAwait(false);
        int pickupOfficeId = await locationService.GetOfficeId(account, pickupCountryId, pickupSiteId, ct).ConfigureAwait(false);

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
}
