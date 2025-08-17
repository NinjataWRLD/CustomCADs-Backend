using CustomCADs.Speedy.Core.Services.Client.Models;
using CustomCADs.Speedy.Core.Services.Location.Block;
using CustomCADs.Speedy.Core.Services.Location.Complex;
using CustomCADs.Speedy.Core.Services.Location.Country;
using CustomCADs.Speedy.Core.Services.Location.Office;
using CustomCADs.Speedy.Core.Services.Location.Poi;
using CustomCADs.Speedy.Core.Services.Location.Site;
using CustomCADs.Speedy.Core.Services.Location.State;
using CustomCADs.Speedy.Core.Services.Location.Street;
using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Recipient;
using CustomCADs.Speedy.Core.Services.Models.Calculation.Sender;
using CustomCADs.Speedy.Core.Services.Models.Shipment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Parcel;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Secondary;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Speedy.Core.Services.Services.Models;
using CustomCADs.Speedy.Core.Services.Shipment.Models;
using CustomCADs.Speedy.Core.Services.Track.Models;
using CustomCADs.Speedy.Http.Enums;

namespace CustomCADs.Speedy.Sdk;

using Core.Contracts.Calculation;
using Core.Contracts.Client;
using Core.Contracts.Location;
using Core.Contracts.Payment;
using Core.Contracts.Pickup;
using Core.Contracts.Print;
using Core.Contracts.Services;
using Core.Contracts.Shipment;
using Core.Contracts.Track;
using Core.Contracts.Validation;

internal class SpeedyService(
	SpeedyOptions options,
	ICalculationService calculation,
	IClientService client,
	ILocationService location,
	IPaymentService payment,
	IPickupService pickup,
	IPrintService print,
	IServicesService services,
	IShipmentService shipment,
	ITrackService track,
	IValidationService validation
) : ISpeedyService
{
	#region Calculation
	public async Task<CalculateModel[]> CalculateAsync(
		Payer payer,
		double[] weights,
		string country,
		string site,
		string street,
		CancellationToken ct = default
	) => await calculation.CalculateAsync(
			account: options.Account,
			pickup: options.Pickup,
			payer: payer,
			weights: weights,
			country: country,
			site: site,
			street: street,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Client
	public async Task<ContactInfoModel> ContractInfoAsync(
		CancellationToken ct = default
	) => await client.ContractInfoAsync(options.Account, ct).ConfigureAwait(false);

	public async Task<long> CreateContactAsync(
		string externalContactId,
		PhoneNumberModel phone1,
		string clientName,
		bool privatePerson,
		ShipmentAddressModel address,
		string? secretKey = null,
		PhoneNumberModel? phone2 = null,
		string? objectName = null,
		string? email = null,
		CancellationToken ct = default
	) => await client.CreateContactAsync(
			account: options.Account,
			externalContactId: externalContactId,
			phone1: phone1,
			clientName: clientName,
			privatePerson: privatePerson,
			address: address,
			secretKey: secretKey,
			phone2: phone2,
			objectName: objectName,
			email: email,
			ct: ct
		).ConfigureAwait(false);

	public async Task<ClientModel> GetClientAsync(
		long clientId,
		CancellationToken ct = default
	) => await client.GetClientAsync(options.Account, clientId, ct).ConfigureAwait(false);

	public async Task<ClientModel> GetContactByExternalIdAsync(
		long id,
		string? key = null,
		CancellationToken ct = default
	) => await client.GetContactByExternalIdAsync(options.Account, id, key, ct).ConfigureAwait(false);

	public async Task<ClientModel[]> GetContractClientsAsync(
		CancellationToken ct = default
	) => await client.GetContractClientsAsync(options.Account, ct).ConfigureAwait(false);

	public async Task<long> GetOwnClientIdAsync(
		CancellationToken ct = default
	) => await client.GetOwnClientIdAsync(options.Account, ct).ConfigureAwait(false);
	#endregion

	#region Location
	public async Task<BlockModel[]> FindBlockAsync(
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await location.FindBlockAsync(options.Account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<ComplexModel[]> FindComplexAsync(
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await location.FindComplexAsync(options.Account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<CountryModel[]> FindCountryAsync(
		string? name = null,
		string? isoAlpha2 = null,
		string? isoAlpha3 = null,
		CancellationToken ct = default
	) => await location.FindCountryAsync(options.Account, name, isoAlpha2, isoAlpha3, ct).ConfigureAwait(false);

	public async Task<OfficeModel[]> FindOfficeAsync(
		int? countryId = null,
		long? siteId = null,
		string? name = null,
		string? siteName = null,
		int? limit = null,
		CancellationToken ct = default
	) => await location.FindOfficeAsync(options.Account, countryId, siteId, name, siteName, limit, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel[]> FindPointOfInterestAsync(
		int siteId,
		string? name = null,
		CancellationToken ct = default
	) => await location.FindPointOfInterestAsync(options.Account, siteId, name, ct).ConfigureAwait(false);

	public async Task<SiteModel[]> FindSiteAsync(
		int countryId,
		string? name = null,
		string? type = null,
		string? postCode = null,
		string? municipality = null,
		string? region = null,
		CancellationToken ct = default
	) => await location.FindSiteAsync(options.Account, countryId, name, type, postCode, municipality, region, ct).ConfigureAwait(false);

	public async Task<StateModel[]> FindStateAsync(
		int countryId,
		string? name = null,
		CancellationToken ct = default
	) => await location.FindStateAsync(options.Account, countryId, name, ct).ConfigureAwait(false);

	public async Task<StreetModel[]> FindStreetAsync(
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await location.FindStreetAsync(options.Account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetBlocksAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetBlocksAsync(options.Account, countryId, ct).ConfigureAwait(false);

	public async Task<ComplexModel> GetComplexAsync(
		long id,
		CancellationToken ct = default
	) => await location.GetComplexAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetComplexesAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetComplexesAsync(options.Account, countryId, ct).ConfigureAwait(false);

	public async Task<byte[]> GetCountriesAsync(
		CancellationToken ct = default
	) => await location.GetCountriesAsync(options.Account, ct).ConfigureAwait(false);

	public async Task<CountryModel> GetCountryAsync(
		int id,
		CancellationToken ct = default
	) => await location.GetCountryAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<OfficeModel> GetOfficeAsync(
		int id,
		CancellationToken ct = default
	) => await location.GetOfficeAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(
		FindNeaerestOfficeModel model,
		CancellationToken ct = default
	) => await location.GetOfficeAsync(options.Account, model, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel> GetPointOfInterestAsync(
		int id,
		CancellationToken ct = default
	) => await location.GetPointOfInterestAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPointsOfInterestAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetPointsOfInterestAsync(options.Account, countryId, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPostCodesAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetPostCodesAsync(options.Account, countryId, ct).ConfigureAwait(false);

	public async Task<SiteModel> GetSiteAsync(
		long id,
		CancellationToken ct = default
	) => await location.GetSiteAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetSitesAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetSitesAsync(options.Account, countryId, ct).ConfigureAwait(false);

	public async Task<StateModel> GetStateAsync(
		string id,
		CancellationToken ct = default
	) => await location.GetStateAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStatesAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetStatesAsync(options.Account, countryId, ct).ConfigureAwait(false);

	public async Task<StreetModel> GetStreetAsync(
		long id,
		CancellationToken ct = default
	) => await location.GetStreetAsync(options.Account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStreetsAsync(
		int countryId,
		CancellationToken ct = default
	) => await location.GetStreetsAsync(options.Account, countryId, ct).ConfigureAwait(false);
	#endregion

	#region Payment
	public async Task<PayoutModel[]> Payout(
		DateTime fromDate,
		DateTime toDate,
		bool? includeDetails = null,
		CancellationToken ct = default
	) => await payment.Payout(
		account: options.Account,
		fromDate: fromDate,
		toDate: toDate,
		includeDetails: includeDetails,
		ct: ct
	).ConfigureAwait(false);
	#endregion

	#region Pickup
	public async Task<PickupModel[]> Pickup(
		TimeOnly visitEndTime,
		PickupScope pickupScope = PickupScope.EXPLICIT_SHIPMENT_ID_LIST,
		DateTime? pickupDateTime = null,
		bool? autoAdjustPickupDate = null,
		string[]? explicitShipmentIdList = null,
		string? contactName = null,
		string? phoneNumber = null,
		CancellationToken ct = default
	) => await pickup.Pickup(
			account: options.Account,
			visitEndTime: visitEndTime,
			pickupScope: pickupScope,
			pickupDateTime: pickupDateTime,
			autoAdjustPickupDate: autoAdjustPickupDate,
			explicitShipmentIdList: explicitShipmentIdList,
			contactName: contactName,
			phoneNumber: phoneNumber,
			ct: ct
		).ConfigureAwait(false);

	public async Task<string[]> PickupTerms(
		int serviceId,
		DateOnly? startingDate = null,
		CalculationSenderModel? sender = null,
		bool senderHasPayment = false,
		CancellationToken ct = default
	) => await pickup.PickupTerms(
			account: options.Account,
			serviceId: serviceId,
			startingDate: startingDate,
			sender: sender,
			senderHasPayment: senderHasPayment,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Print
	public async Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(
		string shipmentId,
		PaperSize paperSize,
		PaperFormat format = PaperFormat.pdf,
		Dpi dpi = Dpi.dpi203,
		AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE,
		string? printerName = null,
		CancellationToken ct = default
	) => await print.ExtendedPrintAsync(
			account: options.Account,
			contact: options.Contact,
			shipmentId: shipmentId,
			paperSize: paperSize,
			format: format,
			dpi: dpi,
			additionalWaybillSenderCopy: additionalWaybillSenderCopy,
			printerName: printerName,
			ct: ct
		).ConfigureAwait(false);

	public async Task<LabelInfoModel[]> LabelInfoAsync(
		ShipmentParcelRefModel[] parcels,
		CancellationToken ct = default
	) => await print.LabelInfoAsync(options.Account, parcels, ct).ConfigureAwait(false);

	public async Task<byte[]> PrintAsync(
		string shipmentId,
		PaperSize paperSize = PaperSize.A4,
		PaperFormat format = PaperFormat.pdf,
		Dpi dpi = Dpi.dpi203,
		AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE,
		string? printerName = null,
		CancellationToken ct = default
	) => await print.PrintAsync(
			account: options.Account,
			contact: options.Contact,
			shipmentId: shipmentId,
			paperSize: paperSize,
			format: format,
			dpi: dpi,
			additionalWaybillSenderCopy: additionalWaybillSenderCopy,
			printerName: printerName,
			ct: ct
		).ConfigureAwait(false);

	public async Task<byte[]> PrintVoucherAsync(
		string[] shipmentIds,
		string? printerName = null,
		PaperFormat format = PaperFormat.pdf,
		Dpi dpi = Dpi.dpi203,
		CancellationToken ct = default
	) => await print.PrintVoucherAsync(
			account: options.Account,
			shipmentIds: shipmentIds,
			printerName: printerName,
			format: format,
			dpi: dpi,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Services
	public async Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(
		CalculationRecipientModel recipient,
		DateOnly? date = null,
		CalculationSenderModel? sender = null,
		CancellationToken ct = default
	) => await services.DestinationServices(
			account: options.Account,
			recipient: recipient,
			date: date,
			sender: sender,
			ct: ct
		).ConfigureAwait(false);

	public async Task<CourierServiceModel[]> Services(
		DateOnly? date = null,
		CancellationToken ct = default
	) => await services.Services(options.Account, date, ct).ConfigureAwait(false);
	#endregion

	#region Shipment
	public async Task<CreatedShipmentParcelModel> AddParcelAsync(
		string shipmentId,
		ShipmentParcelModel parcel,
		ShipmentCodFiscalReceiptItemModel[] codFiscalReceiptItems,
		double declaredValueAmount,
		double? codAmount = null,
		CancellationToken ct = default
	) => await shipment.AddParcelAsync(
		account: options.Account,
		shipmentId: shipmentId,
		parcel: parcel,
		codFiscalReceiptItems: codFiscalReceiptItems,
		declaredValueAmount: declaredValueAmount,
		codAmount: codAmount,
		ct: ct
	).ConfigureAwait(false);

	public async Task<BarcodeInformationModel> BarcodeInformationAsync(
		ShipmentParcelRefModel parcel,
		CancellationToken ct = default
	) => await shipment.BarcodeInformationAsync(options.Account, parcel, ct).ConfigureAwait(false);

	public async Task CancelShipmentAsync(
		string shipmentId,
		string comment,
		CancellationToken ct = default
	) => await shipment.CancelShipmentAsync(options.Account, shipmentId, comment, ct).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> CreateShipmentAsync(
		string package,
		string contents,
		int parcelCount,
		Payer payer,
		double totalWeight,
		string country,
		string site,
		string street,
		string name,
		string service,
		string? email,
		string? phoneNumber,
		CancellationToken ct = default
	) => await shipment.CreateShipmentAsync(
		account: options.Account,
		pickup: options.Pickup,
		contact: options.Contact,
		package: package,
		contents: contents,
		parcelCount: parcelCount,
		payer: payer,
		totalWeight: totalWeight,
		country: country,
		site: site,
		street: street,
		name: name,
		service: service,
		email: email,
		phoneNumber: phoneNumber,
		ct: ct
	).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> FinalizePendingShipmentAsync(
		string shipmentId,
		CancellationToken ct = default
	) => await shipment.FinalizePendingShipmentAsync(options.Account, shipmentId, ct).ConfigureAwait(false);

	public async Task<string[]> FindParcelsByRefAsync(
		string @ref,
		int searchInRef,
		bool? shipmentsOnly = null,
		bool? includeReturns = null,
		DateTime? fromDateTime = null,
		DateTime? toDateTime = null,
		CancellationToken ct = default
	) => await shipment.FindParcelsByRefAsync(
		account: options.Account,
		@ref: @ref,
		searchInRef: searchInRef,
		shipmentsOnly: shipmentsOnly,
		includeReturns: includeReturns,
		fromDateTime: fromDateTime,
		toDateTime: toDateTime,
		ct: ct
	).ConfigureAwait(false);

	public async Task HandoverToCourierAsync(
		(DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels,
		CancellationToken ct = default
	) => await shipment.HandoverToCourierAsync(options.Account, parcels, ct).ConfigureAwait(false);

	public async Task HandoverToMidwayCarrierAsync(
		(DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels,
		CancellationToken ct = default
	) => await shipment.HandoverToMidwayCarrierAsync(options.Account, parcels, ct).ConfigureAwait(false);

	public async Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(
		string shipmentId,
		ShipmentType[] types,
		CancellationToken ct = default
	) => await shipment.SecondaryShipmentAsync(options.Account, shipmentId, types, ct).ConfigureAwait(false);

	public async Task<ShipmentModel[]> ShipmentInfoAsync(
		string[] shipmentIds,
		CancellationToken ct = default
	) => await shipment.ShipmentInfoAsync(
			account: options.Account,
			contact: options.Contact,
			shipmentIds: shipmentIds,
			ct: ct
		).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> UpdateShipmentAsync(
		string shipmentId,
		WriteShipmentModel model,
		CancellationToken ct = default
	) => await shipment.UpdateShipmentAsync(options.Account, shipmentId, model, ct).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> UpdateShipmentPropertiesAsync(
		string shipmentId,
		Dictionary<string, string> properties,
		CancellationToken ct = default
	) => await shipment.UpdateShipmentPropertiesAsync(
			account: options.Account,
			shipmentId: shipmentId,
			properties: properties,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Track
	public async Task<(long Id, string Url)[]> BulkTrackingDataFiles(
		long? lastProcessedFileId = null,
		CancellationToken ct = default
	) => await track.BulkTrackingDataFiles(
			account: options.Account,
			lastProcessedFileId: lastProcessedFileId,
			ct: ct
		).ConfigureAwait(false);

	public async Task<TrackedParcelModel[]> TrackAsync(
		string shipmentId,
		bool? lastOperationOnly = null,
		CancellationToken ct = default
	) => await track.TrackAsync(
			account: options.Account,
			contact: options.Contact,
			shipmentId: shipmentId,
			lastOperationOnly: lastOperationOnly,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Validation
	public async Task<bool> ValidateAddress(
		ShipmentAddressModel address,
		CancellationToken ct = default
	) => await validation.ValidateAddress(
			account: options.Account,
			address: address,
			ct: ct
		).ConfigureAwait(false);

	public async Task<bool> ValidatePhone(
		PhoneNumberModel phoneNumber,
		CancellationToken ct = default
	) => await validation.ValidatePhone(
			account: options.Account,
			phoneNumber: phoneNumber,
			ct: ct
		).ConfigureAwait(false);

	public async Task<bool> ValidatePostCode(
		string postCode,
		long? siteId = null,
		CancellationToken ct = default
	) => await validation.ValidatePostCode(
			account: options.Account,
			postCode: postCode,
			siteId: siteId,
			ct: ct
		).ConfigureAwait(false);

	public async Task<bool> ValidateShipment(
		WriteShipmentModel shipment,
		CancellationToken ct = default
	) => await validation.ValidateShipment(
			account: options.Account,
			shipment: shipment,
			ct: ct
		).ConfigureAwait(false);
	#endregion
}
