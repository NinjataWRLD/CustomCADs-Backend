namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.GetSite;

public record GetSiteRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);