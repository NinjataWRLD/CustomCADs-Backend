namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent;

using ShipmentParcel;

/// <param name="Contents">
///     Shipment content’s description.
///     <br />
///     Required: Yes. Max length: 100.
/// </param>
/// <param name="Package">
///     Shipment package.
///     <br />
///     Required: Yes. Max length: 50.
/// </param>
/// <param name="ParcelsCount">
///     Total shipment parcels count. Ignored if parcels list is not empty.
///     <br />
///     Required: when parcels list is empty.
/// </param>
/// <param name="TotalWeight">
///     Total weight declared for the shipment. Ignored if parcels list is not empty.
///     <br />
///     Required: when parcels list is empty.
/// </param>
/// <param name="Documents">
///     Documents flag of the shipment.
///     <br />
///     Required: No.
/// </param>
/// <param name="Palletized">
///     Palletized flag of the shipment.
///     <br />
///     Required: No.
/// </param>
/// <param name="Parcels">
///     List of parcels.
///     <br />
///     Required: for pallet and postal services.
/// </param>
/// <param name="PendingParcels">
///     If true, shipment is created in pending parcels state.
///     <br />
///     Required: No.
/// </param>
/// <param name="ExciseGoods">
///     Flag shipment contains excise goods.
///     <br />
///     Required: No.
/// </param>
/// <param name="Iq">
///     Flag shipment contains dangerous goods in limited quantities.
///     <br />
///     Required: No.
/// </param>
/// <param name="GoodsValue">
///     Shipment goods value amount.
///     <br />
///     Required: No. Required for pallet services (Romanian shipments).
/// </param>
/// <param name="GoodsValueCurrencyCode">
///     Shipment goods value currency code.
///     <br />
///     Required: No. Required if GoodsValue is provided.
/// </param>
/// <param name="UitCode">
///     Shipment UIT code.
///     <br />
///     Required: No. Required for Romanian shipments in cases defined by regulations.
/// </param>
internal record ShipmentContentDto(
	string Contents,
	string Package,
	int? ParcelsCount,
	double? TotalWeight,
	bool? Documents,
	bool? Palletized,
	ShipmentParcelDto[]? Parcels,
	bool? PendingParcels,
	bool? ExciseGoods,
	bool? Iq,
	double? GoodsValue,
	string? GoodsValueCurrencyCode,
	string? UitCode
);
