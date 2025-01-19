namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;

public record MidwayCarrierParcelHandoverDto(
    string Id,
    long CountryId,
    string DateTime,
    string? SiteName
);
