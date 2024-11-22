namespace CustomCADs.Shared.Speedy.Models.Calculation.Content;

public record CalculationContentModel(
    int? ParcelsCount,
    double? TotalWeight,
    bool? Documents,
    bool? Palletized,
    ShipmentParcelModel[]? Parcels
);
