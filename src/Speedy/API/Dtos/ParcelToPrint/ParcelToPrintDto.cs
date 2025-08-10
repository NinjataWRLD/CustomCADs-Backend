namespace CustomCADs.Speedy.API.Dtos.ParcelToPrint;

using ShipmentParcels;

public record ParcelToPrintDto(
	ShipmentParcelRefDto Parcel,
	ParcelToPrintAdditionalBarcodeDto? AdditionalBarcode
);
