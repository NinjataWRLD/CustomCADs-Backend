namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.FindSite;

public record FindSiteRequest(
    string UserName,
    string Password,
    int CountryId,
    string? Location,
    long? ClientSystemId,
    string? Name,
    string? PostCode,
    string? Type,
    string? Municipality,
    string? Region
);