using CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Parcel;

public static class Mapper
{
    public static ShipmentParcelRefDto ToDto(this ShipmentParcelRefModel model)
        => new(
            Id: model.Id,
            ExternalCarrierParcelNumber: model.ExternalCarrierParcelNumber,
            FullBarcode: model.FullBarcode
        );
}
