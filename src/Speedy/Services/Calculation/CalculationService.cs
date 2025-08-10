using CustomCADs.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Speedy.API.Endpoints.CalculationEndpoints.Calculation;
using CustomCADs.Speedy.Services.Client;
using CustomCADs.Speedy.Services.Location;
using CustomCADs.Speedy.Services.Models;
using CustomCADs.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Speedy.Services.Services;
using CustomCADs.Speedy.Services.Shipment.Models;

namespace CustomCADs.Speedy.Services.Calculation;

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
	public const string PickupSite = "Sofia";
	public const string PickupStreet = "Flora";

	public async Task<(string Service, ShipmentAdditionalServicesModel? AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, DateTimeOffset DeliveryDeadline)[]> CalculateAsync(
		AccountModel account,
		Payer payer,
		double[] weights,
		string country,
		string site,
		string street,
		CancellationToken ct = default)
	{
		int dropoffCountryId = await locationService.GetCountryId(account, country, ct).ConfigureAwait(false);
		int pickupCountryId = await locationService.GetCountryId(account, PickupCountry, ct).ConfigureAwait(false);

		long dropoffSiteId = await locationService.GetSiteId(account, dropoffCountryId, site, ct).ConfigureAwait(false);
		long pickupSiteId = await locationService.GetSiteId(account, pickupCountryId, PickupSite, ct).ConfigureAwait(false);

		long dropoffStreetId = await locationService.GetStreetId(account, dropoffSiteId, street, ct).ConfigureAwait(false);
		long pickupStreetId = await locationService.GetStreetId(account, pickupSiteId, PickupStreet, ct).ConfigureAwait(false);

		var dropoffOffice = await locationService.GetOfficeId(account, dropoffCountryId, dropoffSiteId, dropoffStreetId, ct).ConfigureAwait(false);
		var pickupOffice = await locationService.GetOfficeId(account, pickupCountryId, pickupSiteId, pickupStreetId, ct).ConfigureAwait(false);

		long clientId = await clientService.GetOwnClientIdAsync(account, ct).ConfigureAwait(false);
		var services = await servicesService.Services(account, null, ct).ConfigureAwait(false);

		CalculationRequest request = new(
			UserName: account.Username,
			Password: account.Password,
			Language: account.Language,
			ClientSystemId: account.ClientSystemId,
			Sender: new(
				ClientId: clientId,
				DropoffOfficeId: dropoffOffice.OfficeId,
				DropoffGeoPUDOId: null, // forbidden
				AddressLocation: null, // forbidden
				PrivatePerson: null // forbidden
			),
			Recipient: new(
				ClientId: clientId,
				PickupOfficeId: pickupOffice.OfficeId,
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
				ParcelsCount: null,
				TotalWeight: null,
				Parcels: [.. weights.Select(weight => weight.ToParcelDto())],
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
		{
			calculations.ForEach(c => c.Error.EnsureNull());
		}

		return [.. calculations.Where(c => c.Error is null).Select(c => c.ToModel(services))];
	}
}
