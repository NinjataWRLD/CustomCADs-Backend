namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Site.GetSite;

using Dtos.Site;

public record GetSiteResponse(
    SiteDto? Site,
    ErrorDto? Error
);