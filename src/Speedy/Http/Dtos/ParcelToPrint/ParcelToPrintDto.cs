namespace CustomCADs.Speedy.Http.Dtos.ParcelToPrint;

using ShipmentParcels;

internal record ParcelToPrintDto(
	ShipmentParcelRefDto Parcel,
	ParcelToPrintAdditionalBarcodeDto? AdditionalBarcode
);
