using CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints.CreateShipment;
using CustomCADs.Shared.Speedy.Services.Calculation;
using CustomCADs.Shared.Speedy.Services.Client;
using CustomCADs.Shared.Speedy.Services.Location;
using CustomCADs.Shared.Speedy.Services.Location.Country;
using CustomCADs.Shared.Speedy.Services.Location.Office;
using CustomCADs.Shared.Speedy.Services.Location.Site;
using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Primary;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Secondary;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Shared.Speedy.Services.Services;
using CustomCADs.Shared.Speedy.Services.Services.Models;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Shipment;

using static Constants;

public class ShipmentService(
    IShipmentEndpoints endpoints,
    LocationService locationService,
    ClientService clientService,
    ServicesService servicesService
)
{
    public const string PhoneNumber1 = "0884874113";
    public const string PhoneNumber2 = "0885440400";
    public const string Email = "customcads2023@gmail.com";
    public const string PickupCountry = "Bulgaria";
    public const string PickupSite = "Burgas";

    public async Task<WrittenShipmentModel> CreateShipmentAsync(
        AccountModel account,
        string package,
        string contents,
        int parcelCount,
        Payer payer,
        double totalWeight,
        string country,
        string site,
        string name,
        string service,
        string? email,
        string? phoneNumber,
        CancellationToken ct = default)
    {
        int dropoffCountryId = await GetCountryId(account, country, ct).ConfigureAwait(false);
        int pickupCountryId = await GetCountryId(account, PickupCountry, ct).ConfigureAwait(false);

        long dropoffSiteId = await GetSiteId(account, dropoffCountryId, site, ct);
        long pickupSiteId = await GetSiteId(account, pickupCountryId, PickupSite, ct);

        int dropoffOfficeId = await GetOfficeId(account, dropoffCountryId, dropoffSiteId, ct).ConfigureAwait(false);
        int pickupOfficeId = await GetOfficeId(account, pickupCountryId, pickupSiteId, ct).ConfigureAwait(false);

        int serviceId = await GetServiceId(account, service, ct).ConfigureAwait(false);
        long clientId = await clientService.GetOwnClientIdAsync(account, ct).ConfigureAwait(false);

        CreateShipmentRequest request = new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentNote: null,
            Sender: new(
                ClientId: clientId,
                DropoffOfficeId: dropoffOfficeId,
                ContactName: name,
                Email: Email,
                Phone1: new(PhoneNumber1, null),
                Phone2: new(PhoneNumber2, null),
                Phone3: null,
                DropoffGeoPUDOId: null, // forbidden
                Address: null, // forbidden
                ClientName: null, // forbidden
                PrivatePerson: null // forbidden
            ),
            Recipient: new(
                ClientId: clientId,
                PickupOfficeId: pickupOfficeId,
                Phone1: phoneNumber is not null ? new(phoneNumber, null) : null,
                Email: email,
                Phone2: null,
                Phone3: null,
                AutoSelectNearestOffice: null,
                AutoSelectNearestOfficePolicy: null,
                ContactName: null,
                ClientName: null, // forbidden
                ObjectName: null, // forbidden
                PrivatePerson: null, // forbidden
                Address: null, // forbidden
                PickupGeoPUDOIf: null // forbidden
            ),
            Service: new(
                ServiceId: serviceId,
                PickupDate: null,
                AdditionalServices: null,
                SaturdayDelivery: null
            ),
            Content: new(
                Package: package,
                Contents: contents,
                ParcelsCount: parcelCount,
                TotalWeight: totalWeight,
                Parcels: null,
                Palletized: null,
                PendingParcels: null,
                Documents: null,
                ExciseGoods: null,
                Iq: null,
                GoodsValue: null,
                GoodsValueCurrencyCode: null,
                UitCode: null
            ),
            Payment: new(
                CourierServicePayer: payer,
                DeclaredValuePayer: null,
                PackagePayer: null,
                ThirdPartyClientId: null,
                DiscountCardId: null,
                SenderBankAccount: null,
                AdministrativeFee: null
            ),

            Id: null,
            Ref1: null,
            Ref2: null,
            ConsolidationRef: null,
            RequireUnsuccessfulDeliveryStickerImage: null
        );

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly[] weekdays = [.. Enumerable.Range(0, 7).Select(today.AddDays)];

        Exception e = new();
        foreach (DateOnly day in weekdays)
        {
            request = request with { Service = request.Service with { PickupDate = day.ToString(DateFormat) } };
            var response = await endpoints.CreateShipmentAsync(request, ct).ConfigureAwait(false);

            try
            {
                response.Error.EnsureNull();
            }
            catch (Exception ex)
            when (ex is SpeedyInvalidPickupOfficeException or SpeedyInvalidDropOffOfficeException)
            {
                e = ex;
                if (weekdays.Last().Day == day.Day) throw;

                continue;
            }

            return new(
                Id: response.Id,
                Parcels: [.. response.Parcels.Select(p => p.ToModel())],
                Price: response.Price.ToModel(),
                PickupDate: DateOnly.Parse(response.PickupDate),
                DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
            );
        }

        throw e;
    }

    private async Task<int> GetServiceId(AccountModel account, string service, CancellationToken ct)
    {
        CourierServiceModel[] services = await servicesService.Services(
            account: account,
            date: null,
            ct: ct
        ).ConfigureAwait(false);

        return services.First(s => s.Name == service || s.NameEn == service).Id;
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

    public async Task CancelShipmentAsync(
        AccountModel account,
        string shipmentId,
        string comment,
        CancellationToken ct = default)
    {
        var response = await endpoints.CancelShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentId: shipmentId,
            Comment: comment
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
    }

    public async Task<CreatedShipmentParcelModel> AddParcelAsync(
        AccountModel account,
        string shipmentId,
        ShipmentParcelModel parcel,
        ShipmentCodFiscalReceiptItemModel[] codFiscalReceiptItems,
        double declaredValueAmount,
        double? codAmount = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.AddParcelShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentId: shipmentId,
            Parcel: parcel.ToDto(),
            DeclaredValueAmount: declaredValueAmount,
            CodFiscalReceiptItems: [.. codFiscalReceiptItems.Select(i => i.ToDto())],
            CodAmount: codAmount
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Parcel.ToModel();
    }

    public async Task<WrittenShipmentModel> FinalizePendingShipmentAsync(
        AccountModel account,
        string shipmentId,
        CancellationToken ct = default)
    {
        var response = await endpoints.FinalizePendingShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentId: shipmentId
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            Id: response.Id,
            Parcels: [.. response.Parcels.Select(p => p.ToModel())],
            Price: response.Price.ToModel(),
            PickupDate: DateOnly.Parse(response.PickupDate),
            DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
        );
    }

    public async Task<ShipmentModel[]> ShipmentInfoAsync(
        AccountModel account,
        string[] shipmentIds,
        CancellationToken ct = default)
    {
        var response = await endpoints.ShipmentInfoAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            ShipmentIds: shipmentIds
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Shipments.Select(d => d.ToModel(PhoneNumber1))];
    }

    public async Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(
        AccountModel account,
        string shipmentId,
        ShipmentType[] types,
        CancellationToken ct = default)
    {
        var response = await endpoints.SecondaryShipmentAsync(shipmentId, new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Types: types
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return [.. response.Shipments.Select(d => d.ToModel())];
    }

    public async Task<WrittenShipmentModel> UpdateShipmentAsync(
        AccountModel account,
        string shipmentId,
        WriteShipmentModel model,
        CancellationToken ct = default)
    {
        var response = await endpoints.UpdateShipmentAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Id: shipmentId,
            Recipient: model.Recipient.ToDto(),
            Service: model.Service.ToDto(),
            Content: model.Content.ToDto(),
            Payment: model.Payment.ToDto(),
            Sender: model.Sender?.ToDto(),
            ShipmentNote: model.ShipmentNote,
            Ref1: model.Ref1,
            Ref2: model.Ref2,
            ConsolidationRef: model.ConsolidationRef,
            RequireUnsuccessfulDeliveryStickerImage: model.RequireUnsuccessfulDeliveryStickerImage
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            Id: response.Id,
            Parcels: [.. response.Parcels.Select(p => p.ToModel())],
            Price: response.Price.ToModel(),
            PickupDate: DateOnly.Parse(response.PickupDate),
            DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
        );
    }

    public async Task<WrittenShipmentModel> UpdateShipmentPropertiesAsync(
        AccountModel account,
        string shipmentId,
        Dictionary<string, string> properties,
        CancellationToken ct = default)
    {
        var response = await endpoints.UpdateShipmentPropertiesAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Id: shipmentId,
            Properties: properties
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            Id: response.Id,
            Parcels: [.. response.Parcels.Select(p => p.ToModel())],
            Price: response.Price.ToModel(),
            PickupDate: DateOnly.Parse(response.PickupDate),
            DeliveryDeadline: DateTime.Parse(response.DeliveryDeadline)
        );
    }

    public async Task<string[]> FindParcelsByRefAsync(
        AccountModel account,
        string @ref,
        int searchInRef,
        bool? shipmentsOnly = null,
        bool? includeReturns = null,
        DateTime? fromDateTime = null,
        DateTime? toDateTime = null,
        CancellationToken ct = default)
    {
        var response = await endpoints.FindParcelsByRefAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Ref: @ref,
            SearchInRef: searchInRef,
            ShipmentsOnly: shipmentsOnly,
            IncludeReturns: includeReturns,
            FromDateTime: fromDateTime?.ToString(DateTimeFormat),
            ToDateTime: toDateTime?.ToString(DateTimeFormat)
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return response.Barcodes;
    }

    public async Task HandoverToCourierAsync(
        AccountModel account,
        (DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels,
        CancellationToken ct = default)
    {
        var response = await endpoints.HandoverToCourierAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
    }

    public async Task HandoverToMidwayCarrierAsync(
        AccountModel account,
        (DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels,
        CancellationToken ct = default)
    {
        var response = await endpoints.HandoverToMidwayCarrierAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcels: [.. parcels.Select(p => p.ToDto())]
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
    }

    public async Task<BarcodeInformationModel> BarcodeInformationAsync(
        AccountModel account,
        ShipmentParcelRefModel parcel,
        CancellationToken ct = default)
    {
        var response = await endpoints.BarcodeInformationAsync(new(
            UserName: account.Username,
            Password: account.Password,
            Language: account.Language,
            ClientSystemId: account.ClientSystemId,
            Parcel: parcel.ToDto()
        ), ct).ConfigureAwait(false);

        response.Error.EnsureNull();
        return new(
            LabelInfo: response.LabelInfo.ToModel(),
            PrimaryShipment: response.PrimaryShipment.ToModel(),
            PrimaryParcelId: response.PrimaryParcelId,
            ReturnShipmentId: response.ReturnShipmentId,
            ReturnParcelId: response.ReturnParcelId,
            RedirectShipmentId: response.RedirectShipmentId,
            RedirectParcelId: response.RedirectParcelId,
            InitialShipmentId: response.InitialShipmentId,
            InitialParcelId: response.InitialParcelId
        );
    }
}
