namespace CustomCADs.Shared.Speedy.API.Dtos.CalculationAddressLocation;

public record CalculationAddressLocationDto(
    int? CountryId,
    string? StateId,
    long? SiteId,
    string? SiteType,
    string? SiteName,
    string? PostCode
);
