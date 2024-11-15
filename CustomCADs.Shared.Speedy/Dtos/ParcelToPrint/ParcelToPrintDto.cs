namespace CustomCADs.Shared.Speedy.Dtos.ParcelToPrint;

using ShipmentParcels;

public record ParcelToPrintDto(
    ShipmentParcelRefDto Parcel,
    ParcelToPrintAdditionalBarcodeDto AdditionalBarcode
);
