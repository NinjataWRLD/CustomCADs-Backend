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
		AccountModel account,
		Payer payer,
		double[] weights,
		string country,
		string site,
		string street,
		CancellationToken ct = default
	) => await calculation.CalculateAsync(
			account: account,
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
		AccountModel account,
		CancellationToken ct = default
	) => await client.ContractInfoAsync(account, ct).ConfigureAwait(false);

	public async Task<long> CreateContactAsync(
		AccountModel account,
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
			account: account,
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
		AccountModel account,
		long clientId,
		CancellationToken ct = default
	) => await client.GetClientAsync(account, clientId, ct).ConfigureAwait(false);

	public async Task<ClientModel> GetContactByExternalIdAsync(
		AccountModel account,
		long id,
		string? key = null,
		CancellationToken ct = default
	) => await client.GetContactByExternalIdAsync(account, id, key, ct).ConfigureAwait(false);

	public async Task<ClientModel[]> GetContractClientsAsync(
		AccountModel account,
		CancellationToken ct = default
	) => await client.GetContractClientsAsync(account, ct).ConfigureAwait(false);

	public async Task<long> GetOwnClientIdAsync(
		AccountModel account,
		CancellationToken ct = default
	) => await client.GetOwnClientIdAsync(account, ct).ConfigureAwait(false);
	#endregion

	#region Location
	public async Task<BlockModel[]> FindBlockAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await location.FindBlockAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<ComplexModel[]> FindComplexAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await location.FindComplexAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<CountryModel[]> FindCountryAsync(
		AccountModel account,
		string? name = null,
		string? isoAlpha2 = null,
		string? isoAlpha3 = null,
		CancellationToken ct = default
	) => await location.FindCountryAsync(account, name, isoAlpha2, isoAlpha3, ct).ConfigureAwait(false);

	public async Task<OfficeModel[]> FindOfficeAsync(
		AccountModel account,
		int? countryId = null,
		long? siteId = null,
		string? name = null,
		string? siteName = null,
		int? limit = null,
		CancellationToken ct = default
	) => await location.FindOfficeAsync(account, countryId, siteId, name, siteName, limit, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel[]> FindPointOfInterestAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		CancellationToken ct = default
	) => await location.FindPointOfInterestAsync(account, siteId, name, ct).ConfigureAwait(false);

	public async Task<SiteModel[]> FindSiteAsync(
		AccountModel account,
		int countryId,
		string? name = null,
		string? type = null,
		string? postCode = null,
		string? municipality = null,
		string? region = null,
		CancellationToken ct = default
	) => await location.FindSiteAsync(account, countryId, name, type, postCode, municipality, region, ct).ConfigureAwait(false);

	public async Task<StateModel[]> FindStateAsync(
		AccountModel account,
		int countryId,
		string? name = null,
		CancellationToken ct = default
	) => await location.FindStateAsync(account, countryId, name, ct).ConfigureAwait(false);

	public async Task<StreetModel[]> FindStreetAsync(
		AccountModel account,
		int siteId,
		string? name = null,
		string? type = null,
		CancellationToken ct = default
	) => await location.FindStreetAsync(account, siteId, name, type, ct).ConfigureAwait(false);

	public async Task<byte[]> GetBlocksAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetBlocksAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<ComplexModel> GetComplexAsync(
		AccountModel account,
		long id,
		CancellationToken ct = default
	) => await location.GetComplexAsync(account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetComplexesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetComplexesAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<byte[]> GetCountriesAsync(
		AccountModel account,
		CancellationToken ct = default
	) => await location.GetCountriesAsync(account, ct).ConfigureAwait(false);

	public async Task<CountryModel> GetCountryAsync(
		AccountModel account,
		int id,
		CancellationToken ct = default
	) => await location.GetCountryAsync(account, id, ct).ConfigureAwait(false);

	public async Task<OfficeModel> GetOfficeAsync(
		AccountModel account,
		int id,
		CancellationToken ct = default
	) => await location.GetOfficeAsync(account, id, ct).ConfigureAwait(false);

	public async Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(
		FindNeaerestOfficeModel model,
		AccountModel account,
		CancellationToken ct = default
	) => await location.GetOfficeAsync(model, account, ct).ConfigureAwait(false);

	public async Task<PointOfInterestModel> GetPointOfInterestAsync(
		AccountModel account,
		int id,
		CancellationToken ct = default
	) => await location.GetPointOfInterestAsync(account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPointsOfInterestAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetPointsOfInterestAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<byte[]> GetPostCodesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetPostCodesAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<SiteModel> GetSiteAsync(
		AccountModel account,
		long id,
		CancellationToken ct = default
	) => await location.GetSiteAsync(account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetSitesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetSitesAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<StateModel> GetStateAsync(
		AccountModel account,
		string id,
		CancellationToken ct = default
	) => await location.GetStateAsync(account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStatesAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetStatesAsync(account, countryId, ct).ConfigureAwait(false);

	public async Task<StreetModel> GetStreetAsync(
		AccountModel account,
		long id,
		CancellationToken ct = default
	) => await location.GetStreetAsync(account, id, ct).ConfigureAwait(false);

	public async Task<byte[]> GetStreetsAsync(
		AccountModel account,
		int countryId,
		CancellationToken ct = default
	) => await location.GetStreetsAsync(account, countryId, ct).ConfigureAwait(false);
	#endregion

	#region Payment
	public async Task<PayoutModel[]> Payout(
		AccountModel account,
		DateTime fromDate,
		DateTime toDate,
		bool? includeDetails = null,
		CancellationToken ct = default
	) => await payment.Payout(
		account: account,
		fromDate: fromDate,
		toDate: toDate,
		includeDetails: includeDetails,
		ct: ct
	).ConfigureAwait(false);
	#endregion

	#region Pickup
	public async Task<PickupModel[]> Pickup(
		AccountModel account,
		TimeOnly visitEndTime,
		PickupScope pickupScope = PickupScope.EXPLICIT_SHIPMENT_ID_LIST,
		DateTime? pickupDateTime = null,
		bool? autoAdjustPickupDate = null,
		string[]? explicitShipmentIdList = null,
		string? contactName = null,
		string? phoneNumber = null,
		CancellationToken ct = default
	) => await pickup.Pickup(
			account: account,
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
		AccountModel account,
		int serviceId,
		DateOnly? startingDate = null,
		CalculationSenderModel? sender = null,
		bool senderHasPayment = false,
		CancellationToken ct = default
	) => await pickup.PickupTerms(
			account: account,
			serviceId: serviceId,
			startingDate: startingDate,
			sender: sender,
			senderHasPayment: senderHasPayment,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Print
	public async Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(
		AccountModel account,
		string shipmentId,
		PaperSize paperSize,
		PaperFormat format = PaperFormat.pdf,
		Dpi dpi = Dpi.dpi203,
		AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE,
		string? printerName = null,
		CancellationToken ct = default
	) => await print.ExtendedPrintAsync(
			account: account,
			shipmentId: shipmentId,
			paperSize: paperSize,
			format: format,
			dpi: dpi,
			additionalWaybillSenderCopy: additionalWaybillSenderCopy,
			printerName: printerName,
			ct: ct
		).ConfigureAwait(false);

	public async Task<LabelInfoModel[]> LabelInfoAsync(
		AccountModel account,
		ShipmentParcelRefModel[] parcels,
		CancellationToken ct = default
	) => await print.LabelInfoAsync(account, parcels, ct).ConfigureAwait(false);

	public async Task<byte[]> PrintAsync(
		AccountModel account,
		string shipmentId,
		PaperSize paperSize = PaperSize.A4,
		PaperFormat format = PaperFormat.pdf,
		Dpi dpi = Dpi.dpi203,
		AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE,
		string? printerName = null,
		CancellationToken ct = default
	) => await print.PrintAsync(
			account: account,
			shipmentId: shipmentId,
			paperSize: paperSize,
			format: format,
			dpi: dpi,
			additionalWaybillSenderCopy: additionalWaybillSenderCopy,
			printerName: printerName,
			ct: ct
		).ConfigureAwait(false);

	public async Task<byte[]> PrintVoucherAsync(
		AccountModel account,
		string[] shipmentIds,
		string? printerName = null,
		PaperFormat format = PaperFormat.pdf,
		Dpi dpi = Dpi.dpi203,
		CancellationToken ct = default
	) => await print.PrintVoucherAsync(
			account: account,
			shipmentIds: shipmentIds,
			printerName: printerName,
			format: format,
			dpi: dpi,
			ct: ct
		).ConfigureAwait(false);
	#endregion

	#region Services
	public async Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(
		AccountModel account,
		CalculationRecipientModel recipient,
		DateOnly? date = null,
		CalculationSenderModel? sender = null,
		CancellationToken ct = default
	) => await services.DestinationServices(
			account: account,
			recipient: recipient,
			date: date,
			sender: sender,
			ct: ct
		).ConfigureAwait(false);

	public async Task<CourierServiceModel[]> Services(
		AccountModel account,
		DateOnly? date = null,
		CancellationToken ct = default
	) => await services.Services(account, date, ct).ConfigureAwait(false);
	#endregion

	#region Shipment
	public async Task<CreatedShipmentParcelModel> AddParcelAsync(
		AccountModel account,
		string shipmentId,
		ShipmentParcelModel parcel,
		ShipmentCodFiscalReceiptItemModel[] codFiscalReceiptItems,
		double declaredValueAmount,
		double? codAmount = null,
		CancellationToken ct = default
	) => await shipment.AddParcelAsync(
		account: account,
		shipmentId: shipmentId,
		parcel: parcel,
		codFiscalReceiptItems: codFiscalReceiptItems,
		declaredValueAmount: declaredValueAmount,
		codAmount: codAmount,
		ct: ct
	).ConfigureAwait(false);

	public async Task<BarcodeInformationModel> BarcodeInformationAsync(
		AccountModel account,
		ShipmentParcelRefModel parcel,
		CancellationToken ct = default
	) => await shipment.BarcodeInformationAsync(account, parcel, ct).ConfigureAwait(false);

	public async Task CancelShipmentAsync(
		AccountModel account,
		string shipmentId,
		string comment,
		CancellationToken ct = default
	) => await shipment.CancelShipmentAsync(account, shipmentId, comment, ct).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> CreateShipmentAsync(
		AccountModel account,
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
		account: account,
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
		AccountModel account,
		string shipmentId,
		CancellationToken ct = default
	) => await shipment.FinalizePendingShipmentAsync(account, shipmentId, ct).ConfigureAwait(false);

	public async Task<string[]> FindParcelsByRefAsync(
		AccountModel account,
		string @ref,
		int searchInRef,
		bool? shipmentsOnly = null,
		bool? includeReturns = null,
		DateTime? fromDateTime = null,
		DateTime? toDateTime = null,
		CancellationToken ct = default
	) => await shipment.FindParcelsByRefAsync(
		account: account,
		@ref: @ref,
		searchInRef: searchInRef,
		shipmentsOnly: shipmentsOnly,
		includeReturns: includeReturns,
		fromDateTime: fromDateTime,
		toDateTime: toDateTime,
		ct: ct
	).ConfigureAwait(false);

	public async Task HandoverToCourierAsync(
		AccountModel account,
		(DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels,
		CancellationToken ct = default
	) => await shipment.HandoverToCourierAsync(account, parcels, ct).ConfigureAwait(false);

	public async Task HandoverToMidwayCarrierAsync(
		AccountModel account,
		(DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels,
		CancellationToken ct = default
	) => await shipment.HandoverToMidwayCarrierAsync(account, parcels, ct).ConfigureAwait(false);

	public async Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(
		AccountModel account,
		string shipmentId,
		ShipmentType[] types,
		CancellationToken ct = default
	) => await shipment.SecondaryShipmentAsync(account, shipmentId, types, ct).ConfigureAwait(false);

	public async Task<ShipmentModel[]> ShipmentInfoAsync(
		AccountModel account,
		string[] shipmentIds,
		CancellationToken ct = default
	) => await shipment.ShipmentInfoAsync(account, shipmentIds, ct).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> UpdateShipmentAsync(
		AccountModel account,
		string shipmentId,
		WriteShipmentModel model,
		CancellationToken ct = default
	) => await shipment.UpdateShipmentAsync(account, shipmentId, model, ct).ConfigureAwait(false);

	public async Task<WrittenShipmentModel> UpdateShipmentPropertiesAsync(
		AccountModel account,
		string shipmentId,
		Dictionary<string, string> properties,
		CancellationToken ct = default
	) => await shipment.UpdateShipmentPropertiesAsync(account, shipmentId, properties, ct).ConfigureAwait(false);
	#endregion

	#region Track
	public async Task<(long Id, string Url)[]> BulkTrackingDataFiles(
		AccountModel account,
		long? lastProcessedFileId = null,
		CancellationToken ct = default
	) => await track.BulkTrackingDataFiles(
		account: account,
		lastProcessedFileId: lastProcessedFileId,
		ct: ct
	).ConfigureAwait(false);

	public async Task<TrackedParcelModel[]> TrackAsync(
		AccountModel account,
		string shipmentId,
		bool? lastOperationOnly = null,
		CancellationToken ct = default
	) => await track.TrackAsync(
		account: account,
		shipmentId: shipmentId,
		lastOperationOnly: lastOperationOnly,
		ct: ct
	).ConfigureAwait(false);
	#endregion

	#region Validation
	public async Task<bool> ValidateAddress(
		AccountModel account,
		ShipmentAddressModel address,
		CancellationToken ct = default
	) => await validation.ValidateAddress(
			account: account,
			address: address,
			ct: ct
		).ConfigureAwait(false);

	public async Task<bool> ValidatePhone(
		AccountModel account,
		PhoneNumberModel phoneNumber,
		CancellationToken ct = default
	) => await validation.ValidatePhone(
			account: account,
			phoneNumber: phoneNumber,
			ct: ct
		).ConfigureAwait(false);

	public async Task<bool> ValidatePostCode(
		AccountModel account,
		string postCode,
		long? siteId = null,
		CancellationToken ct = default
	) => await validation.ValidatePostCode(
			account: account,
			postCode: postCode,
			siteId: siteId,
			ct: ct
		).ConfigureAwait(false);

	public async Task<bool> ValidateShipment(
		AccountModel account,
		WriteShipmentModel shipment,
		CancellationToken ct = default
	) => await validation.ValidateShipment(
			account: account,
			shipment: shipment,
			ct: ct
		).ConfigureAwait(false);
	#endregion
}
