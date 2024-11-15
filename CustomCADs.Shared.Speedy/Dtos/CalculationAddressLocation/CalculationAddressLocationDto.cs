namespace CustomCADs.Shared.Speedy.Dtos.CalculationAddressLocation;

public record CalculationAddressLocationDto(
    int? CountryId,
    string? StateId,
    long? SiteId,
    string? SiteType,
    string? SiteName,
    string? PostCode
);
