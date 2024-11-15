namespace CustomCADs.Shared.Speedy.Dtos.Shipment.Content;

public record ContentDto(
    int ParcelsCount,
    double DeclaredWeight,
    double MeasuredWeight,
    double CalculationWeight,
    string Contents,
    string Package,
    bool Documents,
    bool Palletized,
    ParcelDto Parcels,
    bool PendingParcels
);
