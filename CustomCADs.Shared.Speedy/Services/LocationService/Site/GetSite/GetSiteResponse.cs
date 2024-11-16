using CustomCADs.Shared.Speedy.Dtos.Errors;
using CustomCADs.Shared.Speedy.Dtos.Site;

namespace CustomCADs.Shared.Speedy.Services.LocationService.Site.GetSite;

public record GetSiteResponse(
    SiteDto? Site,
    ErrorDto? Error
);