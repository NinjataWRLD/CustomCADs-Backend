using CustomCADs.Shared.Speedy.API.Dtos.ParcelToPrint;
using CustomCADs.Shared.Speedy.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Print.Models;

namespace CustomCADs.Shared.Speedy.Services.Print;

public static class Mapper
{
    public static ParcelToPrintDto ToDto(this ParcelToPrintModel model)
        => new(
            Parcel: model.Parcel.ToDto(),
            AdditionalBarcode: model.AdditionalBarcode.ToDto()
        );
    
    public static ParcelToPrintAdditionalBarcodeDto ToDto(this ParcelToPrintAdditionalBarcodeModel model)
        => new(
            Value: model.Value,
            Format: model.Format,
            Label: model.Label
        );
}
