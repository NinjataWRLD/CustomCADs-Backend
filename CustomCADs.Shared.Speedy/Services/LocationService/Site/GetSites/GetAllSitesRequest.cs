namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.GetAllSites;

public record GetAllSitesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);