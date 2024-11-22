using CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent;
using CustomCADs.Shared.Speedy.API.Dtos.ShipmentContent.ShipmentParcel;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;

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

    public static ShipmentContentModel ToModel(this ShipmentContentDto dto)
        => new(
            Contents: dto.Contents,
            Package: dto.Package,
            ParcelsCount: dto.ParcelsCount,
            TotalWeight: dto.TotalWeight,
            Documents: dto.Documents,
            Palletized: dto.Palletized,
            Parcels: [.. dto.Parcels?.Select(p => p.ToModel())],
            PendingParcels: dto.PendingParcels,
            ExciseGoods: dto.ExciseGoods,
            Iq: dto.Iq,
            GoodsValue: dto.GoodsValue,
            GoodsValueCurrencyCode: dto.GoodsValueCurrencyCode,
            UitCode: dto.UitCode
        );

    public static ShipmentParcelModel ToModel(this ShipmentParcelDto dto)
        => new(
            Weight: dto.Weight,
            Id: dto.Id,
            SeqNo: dto.SeqNo,
            PackageUniqueNumber: dto.PackageUniqueNumber,
            Ref1: dto.Ref1,
            Ref2: dto.Ref2,
            Size: dto.Size?.ToModel(),
            PickupExternalCarrierParcelNumber: dto.PickupExternalCarrierParcelNumber?.ToModel(),
            DeliveryExternalCarrierParcelNumber: dto.DeliveryExternalCarrierParcelNumber?.ToModel()
        );

    public static ShipmentParcelSizeModel ToModel(this ShipmentParcelSizeDto dto)
        => new(
            Width: dto.Width,
            Depth: dto.Depth,
            Height: dto.Height
        );

    public static ExternalCarrierParcelNumberModel ToModel(this ExternalCarrierParcelNumberDto dto)
        => new(
            ExternalCarrier: dto.ExternalCarrier,
            ParcelNumber: dto.ParcelNumber
        );
}
