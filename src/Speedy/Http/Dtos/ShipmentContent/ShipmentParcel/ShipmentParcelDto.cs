namespace CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;
/// <param name="Weight">
///     Parcel weight in kg.
///     <br />
///     Required: Yes. Validated against courier/depot/service capacity.
/// </param>
/// <param name="Id">
///     Parcel identifier - barcode (if known).
///     <br />
///     Required: No.
/// </param>
/// <param name="SeqNo">
///     Parcel sequence number in the shipment.
///     <br />
///     Required: Yes in createShipment. Ignored in addParcel (auto-generated).
/// </param>
/// <param name="PackageUniqueNumber">
///     Package number associated with parcel.
///     <br />
///     Required: No.
/// </param>
/// <param name="Ref1">
///     Reference number or text.
///     <br />
///     Required: No. Max length: 20.
/// </param>
/// <param name="Ref2">
///     Reference number or text.
///     <br />
///     Required: No. Max length: 20.
/// </param>
/// <param name="Size">
///     Parcel size (width, height, depth) in cm.
///     <br />
///     Required: For pallet and postal services. Validated against courier/depot capacity.
/// </param>
/// <param name="PickupExternalCarrierParcelNumber">
///     External carrier parcel id for pickup.
///     <br />
///     Required: No.
/// </param>
/// <param name="DeliveryExternalCarrierParcelNumber">
///     External carrier parcel id for delivery.
///     <br />
///     Required: No.
/// </param>
/// <param name="Goods">
///     Parcel goods items (for customs control).
///     <br />
///     Required: No. For parcels due to customs control.
/// </param>
internal record ShipmentParcelDto(
	double Weight,
	string? Id,
	int? SeqNo,
	long? PackageUniqueNumber,
	string? Ref1,
	string? Ref2,
	ShipmentParcelSizeDto? Size,
	ExternalCarrierParcelNumberDto? PickupExternalCarrierParcelNumber,
	ExternalCarrierParcelNumberDto? DeliveryExternalCarrierParcelNumber,
	GoodsItemDto[]? Goods
);
