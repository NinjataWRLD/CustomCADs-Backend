using CustomCADs.Speedy.Core.Contracts.Calculation;
using CustomCADs.Speedy.Core.Contracts.Client;
using CustomCADs.Speedy.Core.Contracts.Location;
using CustomCADs.Speedy.Core.Contracts.Payment;
using CustomCADs.Speedy.Core.Contracts.Pickup;
using CustomCADs.Speedy.Core.Contracts.Services;
using CustomCADs.Speedy.Core.Contracts.Shipment;
using CustomCADs.Speedy.Core.Contracts.Track;
using CustomCADs.Speedy.Core.Models;
using CustomCADs.Speedy.Core.Models.Calculation.Recipient;
using CustomCADs.Speedy.Core.Models.Calculation.Sender;
using CustomCADs.Speedy.Core.Models.Shipment;
using CustomCADs.Speedy.Core.Models.Shipment.Parcel;
using CustomCADs.Speedy.Core.Models.Shipment.Secondary;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Speedy.Http.Enums;

namespace CustomCADs.Speedy.Sdk;

public interface ISpeedyService
{
	#region Calculation
	Task<CalculateModel[]> CalculateAsync(Payer payer, double[] weights, string country, string site, string street, CancellationToken ct = default);
	#endregion

	#region Client
	Task<ContactInfoModel> ContractInfoAsync(CancellationToken ct = default);
	Task<long> CreateContactAsync(string externalContactId, PhoneNumberModel phone1, string clientName, bool privatePerson, ShipmentAddressModel address, string? secretKey = null, PhoneNumberModel? phone2 = null, string? objectName = null, string? email = null, CancellationToken ct = default);
	Task<ClientModel> GetClientAsync(long clientId, CancellationToken ct = default);
	Task<ClientModel> GetContactByExternalIdAsync(long id, string? key = null, CancellationToken ct = default);
	Task<ClientModel[]> GetContractClientsAsync(CancellationToken ct = default);
	Task<long> GetOwnClientIdAsync(CancellationToken ct = default);
	#endregion

	#region Location
	Task<BlockModel[]> FindBlockAsync(int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<ComplexModel[]> FindComplexAsync(int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<CountryModel[]> FindCountryAsync(string? name = null, string? isoAlpha2 = null, string? isoAlpha3 = null, CancellationToken ct = default);
	Task<OfficeModel[]> FindOfficeAsync(int? countryId = null, long? siteId = null, string? name = null, string? siteName = null, int? limit = null, CancellationToken ct = default);
	Task<PointOfInterestModel[]> FindPointOfInterestAsync(int siteId, string? name = null, CancellationToken ct = default);
	Task<SiteModel[]> FindSiteAsync(int countryId, string? name = null, string? type = null, string? postCode = null, string? municipality = null, string? region = null, CancellationToken ct = default);
	Task<StateModel[]> FindStateAsync(int countryId, string? name = null, CancellationToken ct = default);
	Task<StreetModel[]> FindStreetAsync(int siteId, string? name = null, string? type = null, CancellationToken ct = default);
	Task<byte[]> GetBlocksAsync(int countryId, CancellationToken ct = default);
	Task<ComplexModel> GetComplexAsync(long id, CancellationToken ct = default);
	Task<byte[]> GetComplexesAsync(int countryId, CancellationToken ct = default);
	Task<byte[]> GetCountriesAsync(CancellationToken ct = default);
	Task<CountryModel> GetCountryAsync(int id, CancellationToken ct = default);
	Task<OfficeModel> GetOfficeAsync(int id, CancellationToken ct = default);
	Task<(int Distance, OfficeModel Office)[]> GetOfficeAsync(FindNeaerestOfficeModel model, CancellationToken ct = default);
	Task<PointOfInterestModel> GetPointOfInterestAsync(int id, CancellationToken ct = default);
	Task<byte[]> GetPointsOfInterestAsync(int countryId, CancellationToken ct = default);
	Task<byte[]> GetPostCodesAsync(int countryId, CancellationToken ct = default);
	Task<SiteModel> GetSiteAsync(long id, CancellationToken ct = default);
	Task<byte[]> GetSitesAsync(int countryId, CancellationToken ct = default);
	Task<StateModel> GetStateAsync(string id, CancellationToken ct = default);
	Task<byte[]> GetStatesAsync(int countryId, CancellationToken ct = default);
	Task<StreetModel> GetStreetAsync(long id, CancellationToken ct = default);
	Task<byte[]> GetStreetsAsync(int countryId, CancellationToken ct = default);
	#endregion

	#region Payment
	Task<PayoutModel[]> Payout(DateTime fromDate, DateTime toDate, bool? includeDetails = null, CancellationToken ct = default);
	#endregion

	#region Pickup
	Task<PickupModel[]> Pickup(TimeOnly visitEndTime, PickupScope pickupScope = PickupScope.EXPLICIT_SHIPMENT_ID_LIST, DateTime? pickupDateTime = null, bool? autoAdjustPickupDate = null, string[]? explicitShipmentIdList = null, string? contactName = null, string? phoneNumber = null, CancellationToken ct = default);
	Task<string[]> PickupTerms(int serviceId, DateOnly? startingDate = null, CalculationSenderModel? sender = null, bool senderHasPayment = false, CancellationToken ct = default);
	#endregion

	#region Print
	Task<(byte[] Data, LabelInfoModel[] PrintLabelsInfo)> ExtendedPrintAsync(string shipmentId, PaperSize paperSize, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE, string? printerName = null, CancellationToken ct = default);
	Task<LabelInfoModel[]> LabelInfoAsync(ShipmentParcelRefModel[] parcels, CancellationToken ct = default);
	Task<byte[]> PrintAsync(string shipmentId, PaperSize paperSize = PaperSize.A4, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, AdditionalWaybillSenderCopy additionalWaybillSenderCopy = AdditionalWaybillSenderCopy.NONE, string? printerName = null, CancellationToken ct = default);
	Task<byte[]> PrintVoucherAsync(string[] shipmentIds, string? printerName = null, PaperFormat format = PaperFormat.pdf, Dpi dpi = Dpi.dpi203, CancellationToken ct = default);
	#endregion

	#region Services
	Task<(string Deadline, CourierServiceModel CourierService)[]> DestinationServices(CalculationRecipientModel recipient, DateOnly? date = null, CalculationSenderModel? sender = null, CancellationToken ct = default);
	Task<CourierServiceModel[]> Services(DateOnly? date = null, CancellationToken ct = default);
	#endregion

	#region Shipment
	Task<CreatedShipmentParcelModel> AddParcelAsync(string shipmentId, ShipmentParcelModel parcel, ShipmentCodFiscalReceiptItemModel[] codFiscalReceiptItems, double declaredValueAmount, double? codAmount = null, CancellationToken ct = default);
	Task<BarcodeInformationModel> BarcodeInformationAsync(ShipmentParcelRefModel parcel, CancellationToken ct = default);
	Task CancelShipmentAsync(string shipmentId, string comment, CancellationToken ct = default);
	Task<WrittenShipmentModel> CreateShipmentAsync(string package, string contents, int parcelCount, Payer payer, double totalWeight, string country, string site, string street, string name, string service, string? email, string? phoneNumber, CancellationToken ct = default);
	Task<WrittenShipmentModel> FinalizePendingShipmentAsync(string shipmentId, CancellationToken ct = default);
	Task<string[]> FindParcelsByRefAsync(string @ref, int searchInRef, bool? shipmentsOnly = null, bool? includeReturns = null, DateTime? fromDateTime = null, DateTime? toDateTime = null, CancellationToken ct = default);
	Task HandoverToCourierAsync((DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels, CancellationToken ct = default);
	Task HandoverToMidwayCarrierAsync((DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels, CancellationToken ct = default);
	Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(string shipmentId, ShipmentType[] types, CancellationToken ct = default);
	Task<ShipmentModel[]> ShipmentInfoAsync(string[] shipmentIds, CancellationToken ct = default);
	Task<WrittenShipmentModel> UpdateShipmentAsync(string shipmentId, WriteShipmentModel model, CancellationToken ct = default);
	Task<WrittenShipmentModel> UpdateShipmentPropertiesAsync(string shipmentId, Dictionary<string, string> properties, CancellationToken ct = default);

	#endregion

	#region Track
	Task<(long Id, string Url)[]> BulkTrackingDataFiles(long? lastProcessedFileId = null, CancellationToken ct = default);
	Task<TrackedParcelModel[]> TrackAsync(string shipmentId, bool? lastOperationOnly = null, CancellationToken ct = default);
	#endregion

	#region Validation
	Task<bool> ValidateAddress(ShipmentAddressModel address, CancellationToken ct = default);
	Task<bool> ValidatePhone(PhoneNumberModel phoneNumber, CancellationToken ct = default);
	Task<bool> ValidatePostCode(string postCode, long? siteId = null, CancellationToken ct = default);
	Task<bool> ValidateShipment(WriteShipmentModel shipment, CancellationToken ct = default);
	#endregion
}
