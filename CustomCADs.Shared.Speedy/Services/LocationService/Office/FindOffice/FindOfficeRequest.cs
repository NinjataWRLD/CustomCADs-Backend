namespace CustomCADs.Shared.Speedy.Services.LocationService.Office.FindOffice;

public record FindOfficeRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId,
    int? CountryId,
    long? SiteId,
    string? SiteName,
    string? Name,
    long? Limit
);