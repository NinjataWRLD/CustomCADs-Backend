namespace CustomCADs.Speedy.Http.Dtos.ParcelToPrint;

using ShipmentParcels;

/// <param name="Parcel">
///     Reference to the parcel to print.
///     <br />
///     Required: Yes. See ShipmentParcelRefDto for details.
/// </param>
/// <param name="AdditionalBarcode">
///     Additional barcode for the parcel, if applicable.
///     <br />
///     Required: No. See ParcelToPrintAdditionalBarcodeDto for details.
/// </param>
internal record ParcelToPrintDto(
	ShipmentParcelRefDto Parcel,
	ParcelToPrintAdditionalBarcodeDto? AdditionalBarcode
);
