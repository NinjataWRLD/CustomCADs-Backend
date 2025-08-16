using CustomCADs.Speedy.Core.Services.Models;
using CustomCADs.Speedy.Core.Services.Models.Shipment;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Parcel;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Secondary;
using CustomCADs.Speedy.Core.Services.Models.Shipment.Service.AdditionalServices.Cod;
using CustomCADs.Speedy.Core.Services.Shipment.Models;

namespace CustomCADs.Speedy.Core.Contracts.Shipment;

internal interface IShipmentService
{
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
}
