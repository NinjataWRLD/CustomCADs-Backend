namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

/// <param name="DateTime">
///     Handover date and time (ISO 8601 format).
///     <br />
///     Required: Yes.
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
internal record ParcelHandoverDto(
	string DateTime,

	// Copied from ShipmentParcelRefDto
	string? Id,
	string? ExternalCarrierParcelNumber,
	string? FullBarcode
);
