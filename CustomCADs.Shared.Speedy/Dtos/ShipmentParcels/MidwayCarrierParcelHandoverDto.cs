namespace CustomCADs.Shared.Speedy.Dtos.ShipmentParcels;

public record MidwayCarrierParcelHandoverDto(
    string Id,
    long CountryId,
    string DateTime,
    string? SiteName
);
