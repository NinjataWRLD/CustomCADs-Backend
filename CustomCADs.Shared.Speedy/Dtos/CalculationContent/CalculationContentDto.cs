namespace CustomCADs.Shared.Speedy.Dtos.CalculationContent;

using ShipmentContent.ShipmentParcel;

public record CalculationContentDto(
    int? ParcelsCount,
    double? TotalWeight,
    bool? Documents,
    bool? Palletized,
    ShipmentParcelDto[]? Parcels
);
