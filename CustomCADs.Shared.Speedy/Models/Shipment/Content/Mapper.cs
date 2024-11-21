using CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent;
using CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent.ShipmentParcel;

namespace CustomCADs.Shared.Speedy.Models.Shipment.Content;

public static class Mapper
{
    public static ShipmentContentDto ToDto(this ShipmentContentModel model)
        => new(
            Contents: model.Contents,
            Package: model.Package,
            ParcelsCount: model.ParcelsCount,
            TotalWeight: model.TotalWeight,
            Documents: model.Documents,
            Palletized: model.Palletized,
            Parcels: [.. model.Parcels?.Select(p => p.ToDto())],
            PendingParcels: model.PendingParcels,
            ExciseGoods: model.ExciseGoods,
            Iq: model.Iq,
            GoodsValue: model.GoodsValue,
            GoodsValueCurrencyCode: model.GoodsValueCurrencyCode,
            UitCode: model.UitCode
        );

    public static ShipmentParcelDto ToDto(this ShipmentParcelModel model)
        => new(
            Weight: model.Weight,
            Id: model.Id,
            SeqNo: model.SeqNo,
            PackageUniqueNumber: model.PackageUniqueNumber,
            Ref1: model.Ref1,
            Ref2: model.Ref2,
            Size: model.Size?.ToDto(),
            PickupExternalCarrierParcelNumber: model.PickupExternalCarrierParcelNumber?.ToDto(),
            DeliveryExternalCarrierParcelNumber: model.DeliveryExternalCarrierParcelNumber?.ToDto()
        );

    public static ShipmentParcelSizeDto ToDto(this ShipmentParcelSizeModel model)
        => new(
            Width: model.Width,
            Depth: model.Depth,
            Height: model.Height
        );

    public static ExternalCarrierParcelNumberDto ToDto(this ExternalCarrierParcelNumberModel model)
        => new(
            ExternalCarrier: model.ExternalCarrier,
            ParcelNumber: model.ParcelNumber
        );
}
