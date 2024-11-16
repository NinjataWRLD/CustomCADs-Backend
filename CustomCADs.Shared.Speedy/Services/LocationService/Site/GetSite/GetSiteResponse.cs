namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.GetSite;

using Dtos.Site;

public record GetSiteResponse(
    SiteDto? Site,
    ErrorDto? Error
);