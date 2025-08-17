using CustomCADs.Speedy.Core.Models;
using CustomCADs.Speedy.Core.Models.Shipment;
using CustomCADs.Speedy.Core.Models.Shipment.Parcel;
using CustomCADs.Speedy.Core.Models.Shipment.Secondary;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices.Cod;

namespace CustomCADs.Speedy.Core.Contracts.Shipment;

internal interface IShipmentService
{
	Task<CreatedShipmentParcelModel> AddParcelAsync(SpeedyAccount account, string shipmentId, ShipmentParcelModel parcel, ShipmentCodFiscalReceiptItemModel[] codFiscalReceiptItems, double declaredValueAmount, double? codAmount = null, CancellationToken ct = default);
	Task<BarcodeInformationModel> BarcodeInformationAsync(SpeedyAccount account, ShipmentParcelRefModel parcel, CancellationToken ct = default);
	Task CancelShipmentAsync(SpeedyAccount account, string shipmentId, string comment, CancellationToken ct = default);
	Task<WrittenShipmentModel> CreateShipmentAsync(SpeedyAccount account, SpeedyPickup pickup, SpeedyContact contact, string package, string contents, int parcelCount, Payer payer, double totalWeight, string country, string site, string street, string name, string service, string? email, string? phoneNumber, CancellationToken ct = default);
	Task<WrittenShipmentModel> FinalizePendingShipmentAsync(SpeedyAccount account, string shipmentId, CancellationToken ct = default);
	Task<string[]> FindParcelsByRefAsync(SpeedyAccount account, string @ref, int searchInRef, bool? shipmentsOnly = null, bool? includeReturns = null, DateTime? fromDateTime = null, DateTime? toDateTime = null, CancellationToken ct = default);
	Task HandoverToCourierAsync(SpeedyAccount account, (DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels, CancellationToken ct = default);
	Task HandoverToMidwayCarrierAsync(SpeedyAccount account, (DateTime DateTime, ShipmentParcelRefModel Parcel)[] parcels, CancellationToken ct = default);
	Task<SecondaryShipmentModel[]> SecondaryShipmentAsync(SpeedyAccount account, string shipmentId, ShipmentType[] types, CancellationToken ct = default);
	Task<ShipmentModel[]> ShipmentInfoAsync(SpeedyAccount account, SpeedyContact contact, string[] shipmentIds, CancellationToken ct = default);
	Task<WrittenShipmentModel> UpdateShipmentAsync(SpeedyAccount account, string shipmentId, WriteShipmentModel model, CancellationToken ct = default);
	Task<WrittenShipmentModel> UpdateShipmentPropertiesAsync(SpeedyAccount account, string shipmentId, Dictionary<string, string> properties, CancellationToken ct = default);
}
