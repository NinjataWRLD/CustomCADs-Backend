namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

/// <param name="Ref">
///     Tracking reference for the parcel.
///     <br />
///     Required: No. May be null if not available.
/// </param>
/// <param name="Id">
///     Parcel identifier.
///     <br />
///     Required: No. May be null if not available.
/// </param>
/// <param name="ExternalCarrierParcelNumber">
///     External carrier parcel number, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="FullBarcode">
///     Full barcode of the parcel, if available.
///     <br />
///     Required: No.
/// </param>
internal record TrackShipmentParcelRefDto(
	string? Ref,

	// Copied from ShipmentParcelRefDto
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
