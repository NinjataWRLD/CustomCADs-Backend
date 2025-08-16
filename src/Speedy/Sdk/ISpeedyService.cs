using CustomCADs.Speedy.Core.Contracts.Calculation;
using CustomCADs.Speedy.Core.Contracts.Client;
using CustomCADs.Speedy.Core.Contracts.Payment;
using CustomCADs.Speedy.Core.Contracts.Pickup;
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

public interface ISpeedyService
{
	#region Calculation
	Task<CalculateModel[]> CalculateAsync(AccountModel account, Payer payer, double[] weights, string country, string site, string street, CancellationToken ct = default);
	#endregion

	#region Client
	Task<ContactInfoModel> ContractInfoAsync(AccountModel account, CancellationToken ct = default);
	Task<long> CreateContactAsync(AccountModel account, string externalContactId, PhoneNumberModel phone1, string clientName, bool privatePerson, ShipmentAddressModel address, string? secretKey = null, PhoneNumberModel? phone2 = null, string? objectName = null, string? email = null, CancellationToken ct = default);
	Task<ClientModel> GetClientAsync(AccountModel account, long clientId, CancellationToken ct = default);
	Task<ClientModel> GetContactByExternalIdAsync(AccountModel account, long id, string? key = null, CancellationToken ct = default);
	Task<ClientModel[]> GetContractClientsAsync(AccountModel account, CancellationToken ct = default);
	Task<long> GetOwnClientIdAsync(AccountModel account, CancellationToken ct = default);
	#endregion

	#region Location
	Task<BlockModel[]> FindBlockAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<ComplexModel[]> FindComplexAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<CountryModel[]> FindCountryAsync(AccountModel account, string? name = null, string? isoAlpha2 = null, string? isoAlpha3 = null, CancellationToken ct = default);
	Task<OfficeModel[]> FindOfficeAsync(AccountModel account, int? countryId = null, long? siteId = null, string? name = null, string? siteName = null, int? limit = null, CancellationToken ct = default);
	Task<PointOfInterestModel[]> FindPointOfInterestAsync(AccountModel account, int siteId, string? name = null, CancellationToken ct = default);
	Task<SiteModel[]> FindSiteAsync(AccountModel account, int countryId, string? name = null, string? type = null, string? postCode = null, string? municipality = null, string? region = null, CancellationToken ct = default);
	Task<StateModel[]> FindStateAsync(AccountModel account, int countryId, string? name = null, CancellationToken ct = default);
	Task<StreetModel[]> FindStreetAsync(AccountModel account, int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<byte[]> GetBlocksAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<ComplexModel> GetComplexAsync(AccountModel account, long id, CancellationToken ct = default);
	Task<byte[]> GetComplexesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<byte[]> GetCountriesAsync(AccountModel account, CancellationToken ct = default);
	Task<CountryModel> GetCountryAsync(AccountModel account, int id, CancellationToken ct = default);
	Task<OfficeModel> GetOfficeAsync(AccountModel account, int id, CancellationToken ct = default);
	Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(FindNeaerestOfficeModel model, AccountModel account, CancellationToken ct = default);
	Task<PointOfInterestModel> GetPointOfInterestAsync(AccountModel account, int id, CancellationToken ct = default);
	Task<byte[]> GetPointsOfInterestAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<byte[]> GetPostCodesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<SiteModel> GetSiteAsync(AccountModel account, long id, CancellationToken ct = default);
	Task<byte[]> GetSitesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<StateModel> GetStateAsync(AccountModel account, string id, CancellationToken ct = default);
	Task<byte[]> GetStatesAsync(AccountModel account, int countryId, CancellationToken ct = default);
	Task<StreetModel> GetStreetAsync(AccountModel account, long id, CancellationToken ct = default);
	Task<byte[]> GetStreetsAsync(AccountModel account, int countryId, CancellationToken ct = default);
	#endregion

	#region Payment
	Task<PayoutModel[]> Payout(AccountModel account, DateTime fromDate, DateTime toDate, bool? includeDetails = null, CancellationToken ct = default);
	#endregion

	#region Pickup
	Task<PickupModel[]> Pickup(AccountModel account, TimeOnly visitEndTime, PickupScope pickupScope = PickupScope.EXPLICIT_SHIPMENT_ID_LIST, DateTime? pickupDateTime = null, bool? autoAdjustPickupDate = null, string[]? explicitShipmentIdList = null, string? contactName = null, string? phoneNumber = null, CancellationToken ct = default);
	Task<string[]> PickupTerms(AccountModel account, int serviceId, DateOnly? startingDate = null, CalculationSenderModel? sender = null, bool senderHasPayment = false, CancellationToken ct = default);
	#endregion

	#region Print
	Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(AccountModel account, string shipmentId, PaperSize paperSize, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE, string? printerName = null, CancellationToken ct = default);
	Task<LabelInfoModel[]> LabelInfoAsync(AccountModel account, ShipmentParcelRefModel[] parcels, CancellationToken ct = default);
	Task<byte[]> PrintAsync(AccountModel account, string shipmentId, PaperSize paperSize = PaperSize.A4, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE, string? printerName = null, CancellationToken ct = default);
	Task<byte[]> PrintVoucherAsync(AccountModel account, string[] shipmentIds, string? printerName = null, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, CancellationToken ct = default);
	#endregion

	#region Services
	Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(AccountModel account, CalculationRecipientModel recipient, DateOnly? date = null, CalculationSenderModel? sender = null, CancellationToken ct = default);
	Task<CourierServiceModel[]> Services(AccountModel account, DateOnly? date = null, CancellationToken ct = default);
	#endregion

	#region Shipment
	Task<CreatedShipmentParcelModel> AddParcelAsync(AccountModel account, string shipmentId, ShipmentParcelModel parcel, ShipmentCodFiscalReceiptItemModel[] codFiscalReceiptItems, double declaredValueAmount, double? codAmount = null, CancellationToken ct = default);
	Task<BarcodeInformationModel> BarcodeInformationAsync(AccountModel account, ShipmentParcelRefModel parcel, CancellationToken ct = default);
	Task CancelShipmentAsync(AccountModel account, string shipmentId, string comment, CancellationToken ct = default);
	Task<WrittenShipmentModel> CreateShipmentAsync(AccountModel account, string package, string contents, int parcelCount, Payer payer, double totalWeight, string country, string site, string street, string name, string service, string? email, string? phoneNumber, CancellationToken ct = default);
	Task<WrittenShipmentModel> FinalizePendingShipmentAsync(AccountModel account, string shipmentId, CancellationToken ct = default);
	Task<string[]> FindParcelsByRefAsync(AccountModel account, string @ref, int searchInRef, bool? shipmentsOnly = null, bool? includeReturns = null, DateTime? fromDateTime = null, DateTime? toDateTime = null, CancellationToken ct = default);
	Task HandoverToCourierAsync(AccountModel account, (DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels, CancellationToken ct = default);
	Task HandoverToMidwayCarrierAsync(AccountModel account, (DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels, CancellationToken ct = default);
	Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(AccountModel account, string shipmentId, ShipmentType[] types, CancellationToken ct = default);
	Task<ShipmentModel[]> ShipmentInfoAsync(AccountModel account, string[] shipmentIds, CancellationToken ct = default);
	Task<WrittenShipmentModel> UpdateShipmentAsync(AccountModel account, string shipmentId, WriteShipmentModel model, CancellationToken ct = default);
	Task<WrittenShipmentModel> UpdateShipmentPropertiesAsync(AccountModel account, string shipmentId, Dictionary<string, string> properties, CancellationToken ct = default);

	#endregion

	#region Track
	Task<(long Id, string Url)[]> BulkTrackingDataFiles(AccountModel account, long? lastProcessedFileId = null, CancellationToken ct = default);
	Task<TrackedParcelModel[]> TrackAsync(AccountModel account, string shipmentId, bool? lastOperationOnly = null, CancellationToken ct = default);
	#endregion

	#region Validation
	Task<bool> ValidateAddress(AccountModel account, ShipmentAddressModel address, CancellationToken ct = default);
	Task<bool> ValidatePhone(AccountModel account, PhoneNumberModel phoneNumber, CancellationToken ct = default);
	Task<bool> ValidatePostCode(AccountModel account, string postCode, long? siteId = null, CancellationToken ct = default);
	Task<bool> ValidateShipment(AccountModel account, WriteShipmentModel shipment, CancellationToken ct = default);
	#endregion
}
