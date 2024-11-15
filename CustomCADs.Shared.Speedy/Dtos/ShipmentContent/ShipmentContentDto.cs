namespace CustomCADs.Shared.Speedy.Dtos.ShipmentContent;

using ShipmentParcel;

public record ShipmentContentDto(
    string Contents,
    string Package,
    int? ParcelsCount,
    double? TotalWeight,
    bool? Documents,
    bool? Palletized,
    ShipmentParcelDto[]? Parcels,
    bool? PendingParcels,
    bool? ExciseGoods,
    bool? Iq,
    double? GoodsValue,
    string? GoodsValueCurrencyCode,
    string? UitCode
);
