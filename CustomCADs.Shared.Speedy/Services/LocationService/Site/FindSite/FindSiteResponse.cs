namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.FindSite;

using Dtos.Site;

public record FindSiteResponse(
    SiteDto[]? Sites,
    ErrorDto? Error
);