namespace CustomCADs.Shared.Speedy.API.Dtos.ParcelToPrint;

using ShipmentParcels;

public record ParcelToPrintDto(
    ShipmentParcelRefDto Parcel,
    ParcelToPrintAdditionalBarcodeDto? AdditionalBarcode
);
